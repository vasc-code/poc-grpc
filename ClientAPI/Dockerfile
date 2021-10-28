FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Api/", "Api/"]
COPY ["Application/", "Application/"]
COPY ["Application/Protos", "Application/Protos"]
COPY ["Domain/", "Domain/"]
COPY ["Infrastructure/", "Infrastructure/"]
COPY ["Bootstrap/", "Bootstrap/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]