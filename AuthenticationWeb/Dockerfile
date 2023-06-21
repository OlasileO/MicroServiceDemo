#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AuthenticationWeb/AuthenticationWeb.csproj", "AuthenticationWeb/"]
RUN dotnet restore "AuthenticationWeb/AuthenticationWeb.csproj"
COPY . .
WORKDIR "/src/AuthenticationWeb"
RUN dotnet build "AuthenticationWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationWeb.dll"]