from flask import Flask, request
from flask_cors import CORS, cross_origin
import requests
from urllib3.exceptions import InsecureRequestWarning
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.kdf.pbkdf2 import PBKDF2HMAC
import binascii
import random
import os

from database import access_database, addNewCredentialToDns, addNewDns, findDnsByName, getAllCredentials

app = Flask(__name__)
cors = CORS(app)
app.config['CORS_HEADERS'] = 'Content-Type'

CLIENT_ENDPOINT = "http://localhost:3000"
SERVER_ENDPOINT = "https://localhost:44314/api/chap"
requests.packages.urllib3.disable_warnings(category=InsecureRequestWarning)

@app.route('/authorise', methods=["POST"])
def authorise():
    dns = request.get_json()["dns"]
    if dns == "" or not dns.startswith("http"):
        return "Wrong dns provided", 400, []

    credentials = find_credentials(dns)
    username = credentials[0]
    password = credentials[1]
        
    challenge_reponse = get_challenge_response(username)
    if challenge_reponse[0] != 200:
        print("Invalid credentials")
        return "", challenge_reponse[0], []
    id = challenge_reponse[1]["challengeId"]
    secret = get_secret(password, challenge_reponse[1])
    
    print("Secret aquired: {}".format(secret))
    
    c_index = 0
    failed = False
    answer = secret[c_index]
    while c_index < len(secret):
        validation_response = get_chap_response(id, answer, c_index)
        if validation_response[0] != 200:
            print("Validation error occured. Status code: {}".format(validation_response[0]))
            return "", validation_response[0], []
        if validation_response[1]["token"] != None:
            print("Successful authentication. Token: {}".format(validation_response[1]))
            return validation_response[1]["token"], 200, []
        result = generate_answer(secret, validation_response, failed)
        answer = result[0]
        failed = result[1]
        c_index = c_index + 2
            
def generate_answer(secret, response, failed):
    c_index = response[1]["index"]
    guess = response[1]["answer"]
    if c_index + 1 >= len(secret):
        return (generate_fake_answer(), False)
    
    if secret[c_index] == guess and not failed:
        return (secret[c_index + 1], False)
    
    return (generate_fake_answer(), True)
    
def generate_fake_answer():
    options = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
    return random.choice(options)
    
def get_secret(password, challenge_response):
    challenge = binascii.a2b_base64(challenge_response["challenge"])
    secret_base64 = binascii.b2a_base64(get_hash(password.encode(),challenge)).decode()
    return secret_base64
    
def get_hash(password, salt):
    kdf = PBKDF2HMAC(
    algorithm=hashes.SHA512(),
    length=32,
    salt=salt,
    iterations=10000)
    return kdf.derive(password)

def get_challenge_response(username):
    headers = {
    'Content-type':'application/json', 
    'Accept':'application/json'
    }
    r = requests.post(url="{}/challenge".format(SERVER_ENDPOINT), params={"username": username}, headers=headers, verify=False)
    return (r.status_code, r.json())

def get_chap_response(id, answer, index):
    headers = {
    'Content-type':'application/json', 
    'Accept':'application/json'
    }
    r = requests.post(url="{}/validate".format(SERVER_ENDPOINT), params={"id": id, "answer": answer, "c_index": index}, headers=headers, verify=False)
    return (r.status_code, r.json())

def find_credentials(dns_name):
    dns = findDnsByName(dns_name)
    if dns == None:
        dns = addNewDns(dns_name)
        print("New DNS created! Enter credentials")
        username = input("Username: ")
        password = input("Password: ")
        addNewCredentialToDns(dns["id"], username, password)
        return (username, password)
    else:
        return ask_credentials(dns["credentials"], dns["id"])

def ask_credentials(credentials, id):
    index = 0
    for cred in credentials:
        print("{}. username: {} password: {}".format(index, cred["username"], cred["password"]))
        index += 1
    print("{}. Add new credentials".format(index))
    
    number = int(input("Please enter the number of the credential you want to use: "))
    while True:
        if number > -1 and number <= len(credentials) - 1:
            return (credentials[number]["username"],credentials[number]["password"])
        if number == index:
            username = input("Username: ")
            password = input("Password: ")
            addNewCredentialToDns(id, username, password)
            return (username, password)


def init():
    if os.path.exists("./enc_db"):
        counter = 0
        while counter < 3:
            try:
                password = input("Database password: ")
                access_database(password)
                print("database accessed")
                return True
            except:
                counter += 1
                print("Wrong password!")
        print("Too many attempts. Database deleted")
        os.remove("./enc_db")
        return False
    else:
        password = input("Please enter a password for the new database: ")
        access_database(password)
        return True
    
def run():
    if init():
        app.run(host='127.0.0.1', port=3001)
run()