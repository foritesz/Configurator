FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# --- build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1) csak a csproj-ok (cache-barát)
COPY BackEnd/BackEnd.csproj BackEnd/
COPY DTO/DTO.csproj DTO/

# (opcionális debug) – MOST már léteznek a mappák:
RUN ls -la /src && ls -la /src/DTO && ls -la /src/BackEnd

# 2) restore a backend projektre
RUN dotnet restore BackEnd/BackEnd.csproj

# 3) teljes források
COPY BackEnd/ BackEnd/
COPY DTO/ DTO/

# (opcionális debug)
RUN ls -la /src/DTO && ls -la /src/BackEnd

# 4) publish
RUN dotnet publish BackEnd/BackEnd.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","BackEnd.dll"]




