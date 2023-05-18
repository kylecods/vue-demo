# Vue CRUD application

## Requirements
- Node.js
- .NET 7 SDK

## Installation 

### Frontend
Once installed Node.js run:
``` bash
npm install
```
Run the application is development:
``` bash
npm run dev
```

### Backend

After installing the .NET SDK,

First install the migration tools

```bash
dotnet tool install --global dotnet-ef
```

Navigate to the Infrastructure folder to start creating the database:
```bash 
cd Infrastructure
```
Run the command below to Add migrations:
```bash
dotnet ef migrations add InitialCreate
```

Update your database:
```bash
dotnet ef database update
```

Change directory to the vue-api folder and start the API:

```bash
dotnet build
```

```bash
dotnet run
```