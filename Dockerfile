FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["HenrexLibraryNowAPI/HenrexLibraryNowAPI.csproj", "HenrexLibraryNowAPI/"]
RUN dotnet restore "HenrexLibraryNowAPI/HenrexLibraryNowAPI.csproj"

COPY . .
WORKDIR "/src/HenrexLibraryNowAPI"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "HenrexLibraryNowAPI.dll"]
