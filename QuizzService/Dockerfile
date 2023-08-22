#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QuizzService/QuizzService.csproj", "QuizzService/"]
RUN dotnet restore "QuizzService/QuizzService.csproj"
COPY . .
WORKDIR "/src/QuizzService"
RUN dotnet build "QuizzService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizzService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizzService.dll"]