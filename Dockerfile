#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9291
EXPOSE 553

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["code/UserContactRegistration.Application/UserContactRegistration.Application.csproj", "UserContactRegistration.Application/"]
COPY ["code/UserContactRegistration.Domain/UserContactRegistration.Domain.csproj", "UserContactRegistration.Domain/"]
COPY ["code/UserContactRegistration.Infrastructure/UserContactRegistration.Infrastructure.csproj", "UserContactRegistration.Infrastructure/"]
RUN dotnet restore "UserContactRegistration.Application/UserContactRegistration.Application.csproj"
COPY . .
WORKDIR "/src/code/UserContactRegistration.Application"
RUN dotnet build "UserContactRegistration.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserContactRegistration.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:9291
ENTRYPOINT ["dotnet", "UserContactRegistration.Application.dll"]