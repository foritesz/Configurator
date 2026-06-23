FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish ./FrontEnd/FrontEnd.csproj -c Release -o /out

FROM nginx:1.29.1-alpine
COPY ./FrontEnd/nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /out/wwwroot /usr/share/nginx/html
EXPOSE 80