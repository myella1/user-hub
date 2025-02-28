User Hub

Project Structure -
The solution contains the following projects:
• AddressApi: An API used for address management.
• UserApi: An API used for user management.
• UserGatewayApi: An API acting as a gateway, to facilitate requests between UserApi and AddressApi.


Prerequisites:
Ensure you have the following installed:
• .NET 8.0 SDK
• Visual Studio 2022 or later with the ASP.NET and web development workload


1. Clone the Repository:
git clone https://github.com/myella1/user-hub.git
cd user-hub


2. Build the Solution:
Open user-hub.sln in Visual Studio and build the solution to restore all dependencies and compile the projects.

  Run the APIs using the following commands:
• AddressApi: dotnet run --project AddressApi
• UserApi:  dotnet run --project UserApi
• UserGatewayApi:  dotnet run --project UserGatewayApi


3. Run the Client Application:
• UserApiClient: Set 'UserApiClient' as the startup project and run it.
