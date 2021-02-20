#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AdessoRideShare/AdessoRideShare.csproj", "AdessoRideShare/"]
COPY ["AdessoRideShare.Data.Access/AdessoRideShare.Data.Access.csproj", "AdessoRideShare.Data.Access/"]
COPY ["AdessoRideShare.Data.Model/AdessoRideShare.Data.Model.csproj", "AdessoRideShare.Data.Model/"]
COPY ["AdessoRideShare.Data.Common/AdessoRideShare.Data.Common.csproj", "AdessoRideShare.Data.Common/"]
COPY ["AdessoRideShare.Api.Common/AdessoRideShare.Api.Common.csproj", "AdessoRideShare.Api.Common/"]
COPY ["AdessoRideShare.Api.Model/AdessoRideShare.Api.Model.csproj", "AdessoRideShare.Api.Model/"]
RUN dotnet restore "AdessoRideShare/AdessoRideShare.csproj"
COPY . .
WORKDIR "/src/AdessoRideShare"
RUN dotnet build "AdessoRideShare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdessoRideShare.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdessoRideShare.dll"]
