#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["../ProductStore.RestfulAPI/ProductStore.RestfulAPI.csproj", "../ProductStore.RestfulAPI/"]
RUN dotnet restore "../ProductStore.RestfulAPI/ProductStore.RestfulAPI.csproj"
COPY . .
WORKDIR "/src/../ProductStore.RestfulAPI"
RUN dotnet build "ProductStore.RestfulAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductStore.RestfulAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductStore.RestfulAPI.dll"]