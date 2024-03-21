# DevHouse C#/.net/MySQL Project
## Description

This is an ASP.NET Core Web API project developed for managing projects, developers, teams, and project types. It provides endpoints to perform CRUD (Create, Read, Update, Delete) operations on these entities in a MYSQL Database.

## Setup Instructions

To set up and run the ASP.NET Core Web API, Follow these steps:

1. Clone the reposity to your local machine:
   git clone https://github.com/noroff-backend-2/aug23ft-bet-ca-1-fadezilla

2. Navigate to the project directory:
   run the command "cd 'your-project-directory-name'" in the console.

3. Install the required dependencies:
   run the command "dotnet restore" in the console.

4. Configure the database connection string in the `appsettings.json` file:

   {
   "ConnectionStrings": {
   "DefaultConnection": ""server=localhost;database=YOUR-DATABASE;user=YOUR-USERNAME;password=YOUR-PASSWORD""
   }
   }

5. Apply any database migratiosn:
   run the command "dotnet ef database update" in the console.

## Run the application

To run the application, use the following command:

"dotnet run"

The API will start running on `http://localhost:port`.

## Creating Migrations

If you need to make any changes to the database schema, create a migration using the Entity Framework Core`s migrations feature.

Run the following command to create a new migration:

"dotnet ef migrations add YOUR-MIGRATION-NAME"

then, apply the migration using:

"dotnet ef database update"

## Connection String

The connection string needed to connect to the MYSQL database is defined in the in the `appsettings.json` file under the `"ConnectionStrings"` section:

{
    "ConnectionStrings": {
        "DefaultConnection": "your-connection-string"
    }
}

The connection string should looke like this: `"server=localhost;database=YOUR-DATABASE;user=YOUR-USERNAME;password=YOUR-PASSWORD"`.

Swap out "YOUR-DATABASE", "YOUR-USERNAME", "YOUR-PASSWORD" with your own credentials.

## Endpoint JSON Requests:

### Add a Developer

POST /api/Developer

Example request body:
```json
{
    "id": 0,
    "firstName": "John",
    "lastName": "Doe",
    "roleId": 1,
    "teamId": 2
}
```
### Update a Developer

PUT /api/Developer/{Id}

Example request body:
```json
{
    "id": 2,
    "firstName": "JohannaUpdatedExample",
    "lastName": "DoeUpdatedExample",
    "teamId": 2,
    "roleId": 3
}
```
### Add a Project

POST /api/Projects

Example request body:
```json
{
    "name": "ExampleProject",
    "projectTypeId": 1,
    "teamId": 1
}
```
### Update a Project

PUT /api/Projects/{Id}

Example request body:
```json
{
    "id": 2,
    "name": "ExampleProjectUpdated",
    "projectTypeId": 1,
    "teamId": 1
}
```
### Add a ProjectType

POST /api/ProjectTypes

Example request body:
```json
{
    "name": "ProjectTypeExample"
}
```
### Update a ProjectType

PUT /api/ProjectTypes/{Id}

Example request body:
```json
{
    "id": 2,
    "name": "ProjectTypeUpdatedExample"
}
```
### Add a Role

POST /api/Role

Example request body:
```json
{
    "name": "RoleExample"
}
```
### Update a Role

PUT /api/Role/{Id}

Example request body:
```json
{
    "id": 2,
    "name": "RoleUpdatedExample"
}
```
### Add a Team

POST /api/Team

Example request body:
```json
{
    "name": "TeamExample"
}
```
### Update a Team

PUT /api/Team/{Id}

Example request body:
```json
{
    "id": 3,
    "name": "TeamUpdatedExample"
}
```
## External Libraries/packages Used:
The project uses the following external libraries/packages:

* Entity Framework Core for database operations
* Microsoft.AspNetCore.Mvc for building RESTful APIs.
* MySql.EntityFrameworkCore for MySQL database integration.
