FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal-arm64v8 AS runtime
COPY ./WebApi/bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "WebApi.dll"]