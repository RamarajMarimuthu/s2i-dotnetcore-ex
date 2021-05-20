#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build

LABEL io.k8s.display-name="app name" \
      io.k8s.description="container description..." \
      io.openshift.expose-services="8080:http"

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

WORKDIR /app
COPY ./app/* /app/
RUN adduser -D buildadmin
RUN chown buildadmin:buildadmin /app /app/* 
USER buildadmin
ENTRYPOINT ["dotnet", "app.dll"]
