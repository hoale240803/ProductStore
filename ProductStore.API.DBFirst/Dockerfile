#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development 

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src/
COPY ["ProductStore.API.DBFirst/ProductStore.API.DBFirst.csproj", "/ProductStore.API.DBFirst/"]
RUN dotnet restore "/ProductStore.API.DBFirst/ProductStore.API.DBFirst.csproj"
COPY . .

WORKDIR /src/ProductStore.API.DBFirst/
RUN dotnet build "ProductStore.API.DBFirst.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductStore.API.DBFirst.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductStore.API.DBFirst.dll"]