FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./TorchFireFilms.Identity.csproj", "src/"]
RUN dotnet restore "src/TorchFireFilms.Identity.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TorchFireFilms.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TorchFireFilms.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT ["dotnet", "TorchFireFilms.Identity.dll"]
