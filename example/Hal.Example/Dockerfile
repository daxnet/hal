#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["LICENSE", "LICENSE"]
COPY ["example/Hal.Example/Hal.Example.csproj", "example/Hal.Example/"]
COPY ["src/Hal.AspNetCore/Hal.AspNetCore.csproj", "src/Hal.AspNetCore/"]
COPY ["src/Hal/Hal.csproj", "src/Hal/"]
RUN dotnet restore "example/Hal.Example/Hal.Example.csproj"
COPY . .
WORKDIR "/src/example/Hal.Example"
RUN dotnet build "Hal.Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hal.Example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hal.Example.dll"]