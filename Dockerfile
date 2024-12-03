FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /app
COPY EclipseTest.Api ./EclipseTest.Api
COPY EclipseTest.Application ./EclipseTest.Application
COPY EclipseTest.Domain ./EclipseTest.Domain
COPY EclipseTest.Infrastructure ./EclipseTest.Infrastructure

WORKDIR /app/EclipseTest.Api

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/EclipseTest.Api/out .
ENTRYPOINT [ "dotnet", "EclipseTest.Api.dll" ]
