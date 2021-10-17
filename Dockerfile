#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarRentalApiService.csproj", "."]
RUN dotnet restore "./CarRentalApiService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CarRentalApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentalApiService.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 6000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentalApiService.dll"]