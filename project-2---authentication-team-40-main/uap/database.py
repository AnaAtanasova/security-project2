from tinydb import TinyDB, Query, where
import tinydb_encrypted_jsonstorage as tae
import uuid

db = None
tableDns = None
def access_database(key):
    global db 
    db = TinyDB(encryption_key=key, path="./enc_db", storage=tae.EncryptedJSONStorage)
    global tableDns
    tableDns = db.table("dns")
    
def getAllCredentials():
    return tableDns["credentials"].all()

def getAllDns():
    return tableDns.all()

def findDnsByName(name):
    query = Query()
    return tableDns.get(query.name == name)

def findDnsById(id):
    query = Query()
    return tableDns.get(query.id == id)

def addNewDns(name):
    newDns = {"id": str(uuid.uuid4()), "name": name, "credentials": []}
    tableDns.insert(newDns)
    return newDns
    
def addNewCredentialToDns(id, username, password):
    dns = findDnsById(id)
    dns["credentials"].append({"username": username, "password": password})
    query = Query()
    tableDns.update(dns, query.id == id)

def deleteDNS(id):
    query = Query()
    tableDns.remove(query.id == id)

def deleteCredentials(id,index):
    dns=findDnsById(id)
    new_credentials = dns["credentials"]
    new_credentials.pop(index)
    query = Query()
    tableDns.update({"credentials": new_credentials }, query.id == id)

def getCredentialsByDNS(name):
    dns = findDnsByName(name)
    return dns["credentials"]