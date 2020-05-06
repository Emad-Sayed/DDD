FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Brimo.IDP.Admin/Brimo.IDP.Admin.csproj", "src/Brimo.IDP.Admin/"]
COPY ["src/Brimo.IDP.Admin.EntityFramework.Shared/Brimo.IDP.Admin.EntityFramework.Shared.csproj", "src/Brimo.IDP.Admin.EntityFramework.Shared/"]
COPY ["src/Brimo.IDP.Admin.EntityFramework.SqlServer/Brimo.IDP.Admin.EntityFramework.SqlServer.csproj", "src/Brimo.IDP.Admin.EntityFramework.SqlServer/"]
COPY ["src/Brimo.IDP.Admin.EntityFramework.PostgreSQL/Brimo.IDP.Admin.EntityFramework.PostgreSQL.csproj", "src/Brimo.IDP.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Brimo.IDP.Admin.EntityFramework.MySql/Brimo.IDP.Admin.EntityFramework.MySql.csproj", "src/Brimo.IDP.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/Brimo.IDP.Admin/Brimo.IDP.Admin.csproj"
COPY . .
WORKDIR "/src/src/Brimo.IDP.Admin"
RUN dotnet build "Brimo.IDP.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brimo.IDP.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Brimo.IDP.Admin.dll"]