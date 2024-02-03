FROM node:21 AS frontend
RUN corepack enable
WORKDIR /src
COPY src/LearnMS.React/package.json .
COPY src/LearnMS.React/pnpm-lock.yaml .
COPY src/LearnMS.React/ .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS backend
WORKDIR /src
COPY src/LearnMS.API/LearnMS.API.csproj .
COPY --from=frontend /src/dist ./wwwroot
RUN dotnet restore
COPY src/LearnMS.API/ .
RUN dotnet build -c Release
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 80
WORKDIR /app
COPY --from=backend /app .

ENTRYPOINT ["dotnet", "LearnMS.API.dll"]