# JWT-NetCore-Vue
JWT (JSON Web Token) implementation with .Net Core - VueJs

## Prerequisites
- Visual studio community 2019
- Net Core 3.1+
- MongoDb
- SMTP Server (Gmail SMTP is used in this case)

## Installation
- Create a MongoDb database.
- Execute the scripts in (/back/database) to initialy populate the database
  - Note: You can refer to the readme.md in /back/database to know more about how to run the scripts against the mongodb instance.

## Configuration

### Backend
The configuration of the backend is located in `/back/JWT-NetCore-Vue/appsettings.json`.
You can also override `appsettings.Development.json` and `appsettings.Production.json` that are environment specifics.

### Frontend
The configuration of the frontend is done using dotenv packages.
The configuration is located at `/front/JWT-NetCore-Vue/.env.development` and `/front/JWT-NetCore-Vue/.env.production` depending on the environment.

## Using Curl to test JWT
`curl.exe -k -H "Content-Type: application/json" -X POST -d "{\"username\":\"test\", \"password\":\"test\"}" http://localhost:5001/api/users/auth`
