# Checkout
How users can get started with the project?

Run via docker
- you need to have a docker environment set-up on your machine
- open solution from Checkout.API with VS2022
- Please restore nuget packages and make sure the solution is ready to go
- Go to Checkout directory and open your terminal there(I personally use WT(Powershell 7))
- In powershell console first execute in the next order: 
    - docker compose build
    - docker compose up
- After you run these 2 commands, you should be able to see the swagger doc of api on: http://localhost:8090/swagger/index.html
- Everything should be good to go and db is already setup by a sql initialization script added to mssql container
- In order to close application please use:
        - docker compose down

Run via VS2022
    - Please restore nuget packages and make sure the solution is ready to go
    - Change connection string json from appsettings.json to your local sql server
    - Execute manually initdb.sql script from Checkout/Sqlinit.Setup
    - Application should be available to http://localhost:5251/swagger/index.html
