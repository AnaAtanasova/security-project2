﻿## Project 2 
http://sweet.ua.pt/jpbarraca/course/sio-2122/project-2/
The aim of the project was to implement a CHAP (Challenge-response authentication protocol) as one of the login options for the users. This was done by creating a separate middleware server application between the original client and the server known as UAP (User authentication application). 

### How to start
#### Frontend

 1. navigate to **to-do-frontend**
 2. `npm install`
 3. `npm start`
#### Backend

 1. navigate to **to-do-backend**
 2. `dotnet ef database update --project to-do-backend`
 3. `dotnet start`
#### UAP

 1. navigate to **uap**
 2. `python -m pip install -r requirements.txt` or install with pip manually
 3. `python chap.py` for UAP controllers
 4.  `python manager.py` for managing database with console

### Changes made to the original application
#### Frontend

 - Added a separate button in the login screen for authorizing with UAP
 - Created separate logic for handling UAP responses and showing them as alerts to the client.
 - Separate Axios component for UAP.
#### Backend

 - Removed password hashing from the database, for the purposes of authenticating with UAP.
 - Separate controller for "api/chap"  Chap endpoints.
 - New entity in the database **Challenge** for storing attempts and secrets that were made with CHAP.
 - Implementation of the Chap algorithm.

### UAP
UAP was created as a separate python flask application for receiving and handling incoming login requests.

 - With the start of application User is required to enter a password to access the database of URI's and credentials or provide the password for the new database.
 - The CHAP in UAP is handled through one endpoint.
 - When a request is received User is requested to either select one of the credentials that have been previously used or has an option to enter new credentials.
 - Upon the completion of CHAP a response of the server or UAP (if server was mistrusted) the client receives appropriate response.
UAP also has a manager.py script to manage the entries in the database. It is a console application. Following actions are allowed:
 - See a list of URI
 - Add new URI
 - Delete an URI from database
 - See credentials of selected URI
 - Delete credentials from selected URI
 - Add credentials to selected URI

## Challenge-response authentication protocol
PBKDF2 is being used both on UAP and Server to create a secret. Password is being hashed with a 16bit challenge generated by the server. 
The following logic was implemented for CHAP.
1. User requests to login with Chap (Sends server URI). UAP must be running and the password for the database must already be provided.
2. UAP ask user for the credentials either from the database or to enter new ones.
3. UAP sends the username to the provided URI of the server.
4. Server creates a new Challenge entity with the challenge(16 random bits) that is used as salt to hash the password of the found user. 
5. Server returns the Id of the challenge and the 16 bit secret.
6. UAP uses the same PBKDF2 hashing algorithm with the provided password and challenge as salt to generate secret.
7. Both UAP and Server communicate secret by one character. As long as each of them trust each other secret is being exchanged by character. If either of them receive an incorrect answer, random characters start being sent until the end of the protocol.
8. UAP receives either a JWT token or an error which is being sent to the client.
9. Client displays error or let's the user login with the JWT.
