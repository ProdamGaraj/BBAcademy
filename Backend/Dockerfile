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
# Add new step to copy app's config files that contain the SQL Server connection string
ENTRYPOINT ["dotnet", "Backend.dll"]