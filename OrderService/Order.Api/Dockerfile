#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderService/Order.Api/Order.Api.csproj", "OrderService/Order.Api/"]
COPY ["OrderService/Order.Application/Order.Application.csproj", "OrderService/Order.Application/"]
COPY ["OrderService/Order.Domain/Order.Domain.csproj", "OrderService/Order.Domain/"]
COPY ["OrderService/Order.Infrastructure/Order.Infrastructure.csproj", "OrderService/Order.Infrastructure/"]
COPY ["OrderService/Order.Persistance/Order.Persistance.csproj", "OrderService/Order.Persistance/"]
COPY ["SharedService/Shared/Shared.csproj", "SharedService/Shared/"]
RUN dotnet restore "OrderService/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/OrderService/Order.Api"
RUN dotnet build "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]