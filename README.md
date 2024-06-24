To use this application:

1. Open the Gateway solution.

2. If you want to send notification emails on OrdersCompute, provide the SendGrid API key to `EmailConfig__ApiKey` in `docker-compose.yml`.

3. Run the following commands in PowerShell:
   ```powershell
   dotnet dev-certs https -ep certs\.aspnet\https\aspnetapp.pfx -p 123
   dotnet dev-certs https --trust
   docker-compose up

5. You can test the APIs using either the Swagger UI or by sending requests through the Gateway. Swagger URLs:

IdentityService: https://localhost:6001/swagger/index.html
OrderService: https://localhost:7001/swagger/index.html
PaymentService: https://localhost:8001/swagger/index.html

Gateway: https://localhost:5001/
