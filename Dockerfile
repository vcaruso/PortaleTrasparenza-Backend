FROM enidgt.azurecr.io/endorsed/microsoft/dotnet/aspnet:8.0.1-alpine3.18-amd64

WORKDIR /app

COPY /dist .

ENTRYPOINT ["dotnet", "ENI.VisLims.Api.dll"]



USER appuser
