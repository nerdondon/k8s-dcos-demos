
FROM microsoft/aspnetcore:1.1
MAINTAINER William Don
LABEL Name=dotnet-test-app Version=0.0.1 
ARG source=./publish
WORKDIR /app
EXPOSE 80
COPY $source .
ENTRYPOINT dotnet dotnet-test-app.dll
