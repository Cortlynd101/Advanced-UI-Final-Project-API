FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
 
COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
 
COPY --from=build /app/out .
 
EXPOSE 80
 
CMD ["dotnet", "Dotnet.dll"]  