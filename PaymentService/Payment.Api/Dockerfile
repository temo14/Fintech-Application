FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PaymentService/Payment.Api/Payment.Api.csproj", "PaymentService/Payment.Api/"]
COPY ["PaymentService/Payment.Application/Payment.Application.csproj", "PaymentService/Payment.Application/"]
COPY ["PaymentService/Payment.Domain/Payment.Domain.csproj", "PaymentService/Payment.Domain/"]
COPY ["PaymentService/Payment.Infrastructure/Payment.Infrastructure.csproj", "PaymentService/Payment.Infrastructure/"]
COPY ["PaymentService/Payment.Persistance/Payment.Persistance.csproj", "PaymentService/Payment.Persistance/"]
COPY ["SharedService/Shared/Shared.csproj", "SharedService/Shared/"]
RUN dotnet restore "PaymentService/Payment.Api/Payment.Api.csproj"
COPY . .
WORKDIR "/src/PaymentService/Payment.Api"
RUN dotnet build "./Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Payment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.Api.dll"]