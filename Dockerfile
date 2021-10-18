FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln ./
COPY CarRentalApi/*.csproj ./CarRentalApi/
COPY CarRentalApi.Domain/*.csproj ./CarRentalApi.Domain/
COPY CarRentalApi.Infrastructure/*.csproj ./CarRentalApi.Infrastructure/
COPY CarRentalApi.Domain.Test/*.csproj ./CarRentalApi.Domain.Test/
COPY CarRentalApi.Infrastructure.Test/*.csproj ./CarRentalApi.Infrastructure.Test/

RUN dotnet restore
COPY . .
WORKDIR /src/CarRentalApi
RUN dotnet build -c Release -o /app

WORKDIR /src/CarRentalApi.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/CarRentalApi.Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/CarRentalApi.Domain.Test
RUN dotnet build -c Release -o /app

WORKDIR /src/CarRentalApi.Infrastructure.Test
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
EXPOSE 6000
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CarRentalApi.dll"]