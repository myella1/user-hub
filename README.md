User Hub

Project Structure:
The solution consists of the following projects:
• AddressApi: API for address management.
• UserApi: API for user management.
• UserGatewayApi: Acts as a gateway to facilitate requests between UserApi and AddressApi.


Prerequisites:
Ensure you have the following installed before proceeding:
• .NET 8.0 SDK
• Visual Studio 2022 or later with the ASP.NET and web development workload


Setup Instructions:
1. Clone the Repository
git clone https://github.com/myella1/user-hub.git  
cd user-hub  

2. Build the Solution
• Open user-hub.sln in Visual Studio.
• Build the solution to restore dependencies and compile all projects.

3. Enable HTTPS for Development
To enable HTTPS in your ASP.NET Core application, generate and trust a self-signed SSL certificate:
1. Open Command Prompt or PowerShell.
2. Run the following command:
dotnet dev-certs https --trust  

This command generates and trusts the HTTPS development certificate on your local machine.

4. Run the APIs
Use the following commands to run each API:

Run AddressApi:
dotnet run --project AddressApi  

Run UserApi:
dotnet run --project UserApi  

Run UserGatewayApi:
dotnet run --project UserGatewayApi  

5. Run the UserApiClient Application
• Set UserApiClient as the Startup Project.
• Run it using the IIS Express profile in Visual Studio.


