# Cat Facts Application
This application showcases a blend of modern technologies to provide random cat facts through a filterable grid interface.

# Technologies Used
## Backend:
.NET 9: The core of the backend, providing robust API functionalities.

Hangfire: Integrated for managing and scheduling background jobs, particularly for upserting cat facts data.

## Frontend:
Vue.js: A progressive JavaScript framework used to build the interactive user interface.

AG Grid: Utilized for displaying cat facts in a dynamic and filterable grid format.

## Database:
SQL Server: Used for storing and retrieving cat fact data.

# Features
### Random Cat Facts: 
Displays interesting and random facts about cats.

### Filterable Grid: 
Users can filter cat facts based on different criteria directly in the grid.

### Background Jobs: 
Efficiently handles upserting data using Hangfire to ensure the latest cat facts are available.

# Getting started

## Database:
Using SQL Server, in a databse of your choice run the `Database/CreateCatFactTable.sql` script;

## Backend:
In the `appsettings.json` file of both `.Api` and `.Worker` projects set the `connection strings` to your database;

The job frequency is defined by the `'cronExpression'` in the `.Worker`'s `appsettings.json`, the `default is every minute`;

`Run` both `.Api` and `.Worker` projects.

## Frontend:
Install dependencies: `npm install`

Run: `npm run serve`

# Usage
Access the application in your browser at `http://localhost:8080`.

Enjoy and filter through the delightful and educational cat facts.
