#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the official Microsoft SQL Server image as the base
FROM mcr.microsoft.com/mssql/server:latest

# Set the working directory
WORKDIR /app

# Copy the SQL script to the working directory
COPY setup.sql .

# Run the SQL script during container startup
CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -i setup.sql

# Set the environment variable for the SQL Server sa password
ENV SA_PASSWORD your_password

# Expose the default SQL Server port
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
EXPOSE 80
COPY *.csproj ./
RUN dotnet restore "Backend.csproj"
COPY . ./
RUN dotnet publish Backend.csproj -c Release -o /app/run
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/run .
ENTRYPOINT ["dotnet", "Backend.dll"]