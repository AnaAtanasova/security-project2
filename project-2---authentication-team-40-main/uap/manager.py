from database import access_database, addNewDns, deleteCredentials, deleteDNS, getAllDns, getCredentialsByDNS, addNewCredentialToDns
import os

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

def print_help():
    print("All Commands: \n 0. Show all available DNS \n 1. Add new DNS \n 2. Remove DNS \n 3. Show credentials of DNS \n 4. Add credentials to DNS \n 5. Delete credentials of a DNS \n")

def show_all_dns():
    all_dns = getAllDns()
    index = 0
    for dns in all_dns:
        print("{}. {}".format(index, dns["name"]))
        index += 1

def add_new_dns():
    while True:
        name = input("Enter DNS name: ")
        if name != "":
            if name.startswith("http"):
                newDns = addNewDns(name)
                print("DNS added")
                return
            else:
                print("Enter a valid DNS name")
        else:
            print("Enter valid DNS name")

def remove_dns():
    show_all_dns()
    while True:
        index = int(input("Enter the index of the DNS you want to delete: "))
        all_dns = getAllDns()
        if index >= 0 and index < len(all_dns):
            dns = all_dns[index]
            dns_id = dns["id"]
            deleteDNS(dns_id)
            print("DNS deleted")
            return
        else: 
            print("Enter valid index ")

def edit_credentials(operation):
    show_all_dns()
    while True:
        index = int(input("Enter the index of the DNS you want to see the credentials from: "))
        all_dns = getAllDns()
        if index >= 0 and index < len(all_dns):
                dns = all_dns[index]
                dns_name = dns["name"]
                credentials = getCredentialsByDNS(dns_name)
                if len(credentials) == 0: 
                    print("No credentials")
                    if operation != "add": return
                index_credentials = 0
                for cred in credentials:
                    print("{}. username: {} password: {}".format(index_credentials, cred["username"], cred["password"]))
                    index_credentials += 1
                if operation == "show": return
                else: break
        else: print("Enter valid index")

    if operation == "add":
        while True:
            username = input("Enter username: ")
            if username != "":
                while True:
                    password = input("Enter password: ")
                    if password != "":
                        dns_id = dns["id"]
                        addNewCredentialToDns(dns_id,username,password)
                        print("Credentials added")
                        return
                    else: print("Invalid password")
            else: print("Invalid username")

    if operation == "delete":
        dns_id = dns["id"]
        while True:
            index = int(input("Enter the index of the credentitals you want to delete: "))
            if index >= 0 and index < len(credentials):
                credential=credentials[index]
                credential_username=credential["username"]
                deleteCredentials(dns_id,index)
                print("Credential deleted")
                return
            else: print("Enter valid index")

def manager():
    if init():
        print("Manager of CHAP")
        while True:
            print_help()
            command = int(input("Enter command number or -1 to exit: "))
            if command == 0:
                show_all_dns()
            elif command == 1:
                add_new_dns()
            elif command == 2:
                remove_dns()
            elif command == 3:
                edit_credentials("show")
            elif command == 4:
                edit_credentials("add")
            elif command == 5:
                edit_credentials("delete")
            elif command == -1:
                return
manager()