FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HenrexLibraryNowAPI/HenrexLibraryNowAPI.csproj", "HenrexLibraryNowAPI/"]
RUN dotnet restore "HenrexLibraryNowAPI/HenrexLibraryNowAPI.csproj"

COPY . .
WORKDIR "/src/HenrexLibraryNowAPI"
RUN dotnet build "HenrexLibraryNowAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HenrexLibraryNowAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HenrexLibraryNowAPI.dll"]
