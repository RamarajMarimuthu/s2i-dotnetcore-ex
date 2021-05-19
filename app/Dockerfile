FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

# set noninteractive installation
ENV DEBIAN_FRONTEND noninteractive

RUN apt-get update -y && apt-get install libfontconfig -y
WORKDIR /app
COPY ["app/app.csproj", "app/"]
RUN dotnet restore "app/app.csproj"
COPY . .
WORKDIR "/app/"
RUN dotnet build "app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app.dll"]