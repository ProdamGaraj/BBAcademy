FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
EXPOSE 80
COPY ../WebApi/WebApi.csproj ./WebApi/
COPY ../BLL/BLL.csproj ./BLL/
COPY ../BLL.Models/BLL.Models.csproj ./BLL.Models/
COPY ../Infrastructure/Infrastructure.csproj ./Infrastructure/
RUN dotnet restore "./WebApi/WebApi.csproj"
COPY ../ ./
RUN dotnet publish "./WebApi/WebApi.csproj" -c Release -o /app/run

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/run .
# Add new step to copy app's config files that contain the SQL Server connection string
ENTRYPOINT ["dotnet", "WebApi.dll"]