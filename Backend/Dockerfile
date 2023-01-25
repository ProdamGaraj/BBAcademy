#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

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