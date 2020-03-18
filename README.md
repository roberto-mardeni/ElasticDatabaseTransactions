# Introduction

This repository demonstrates how to take advantage of [distributed transactions across cloud databases](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-elastic-transactions-overview) in a .Net application where MSDTC is no longer available.

## Getting Started

Setup a pipeline in Azure DevOps using the included azure-pipelines.yml file, it is expected that an Azure Resource Manager service connection with the name 'azure' is present.

## Build and Test

Running the pipeline will deploy the following resources:

- Azure SQL Server (dtc-demo1)
  - Azure SQL DB (database1)
  - Communication Link to dtc-demo2
- Azure SQL Server (dtc-demo2)
  - Azure SQL DB (database2)
- App Service Plan
  - App Service

Once deployed, proceed to the web app url and create a new person, the first name & last name are stored in **database1**, the department assignments are stored in **database2**.
