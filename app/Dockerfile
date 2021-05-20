FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
LABEL io.k8s.display-name="app name" \
      io.k8s.description="container description..." \
      io.openshift.expose-services="8080:http"

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["app.csproj", ""]
RUN dotnet restore "./app.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app.dll"]

# Run container by default as user with id 1001 (default)
USER 1001
