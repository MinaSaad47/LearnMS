FROM node:21 AS frontend
RUN npm install -g npm
RUN corepack enable
WORKDIR /src
COPY src/LearnMS.React/package.json .
RUN npm install
COPY src/LearnMS.React/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS backend
WORKDIR /src
COPY src/LearnMS.API/LearnMS.API.csproj .
RUN dotnet restore
COPY src/LearnMS.API/ .
RUN dotnet publish  -c Release -o /app
COPY --from=frontend /src/dist/ /app/ClientApp

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=backend /app .

ENTRYPOINT ["dotnet", "LearnMS.API.dll"]