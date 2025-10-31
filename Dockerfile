#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9291
EXPOSE 553

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["code/SCF_ECS_REST.Application/SCF_ECS_REST.Application.csproj", "SCF_ECS_REST.Application/"]
COPY ["code/SCF_ECS_REST.Domain/SCF_ECS_REST.Domain.csproj", "SCF_ECS_REST.Domain/"]
COPY ["code/SCF_ECS_REST.Infrastructure/SCF_ECS_REST.Infrastructure.csproj", "SCF_ECS_REST.Infrastructure/"]
RUN dotnet restore "SCF_ECS_REST.Application/SCF_ECS_REST.Application.csproj"
COPY . .
WORKDIR "/src/code/SCF_ECS_REST.Application"
RUN dotnet build "SCF_ECS_REST.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCF_ECS_REST.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:9291
ENTRYPOINT ["dotnet", "SCF_ECS_REST.Application.dll"]