FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore Nano35.Storage.Projection.csproj --configfile ./NuGet.config
WORKDIR /src/.
RUN dotnet build Nano35.Storage.Projection.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Nano35.Storage.Projection.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

ENTRYPOINT ["dotnet", "Nano35.Storage.Projection.dll"]