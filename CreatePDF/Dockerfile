#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update
RUN apt-get install wget libgdiplus -y
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CreatePDF/CreatePDF.csproj", "CreatePDF/"]
RUN dotnet restore "CreatePDF/CreatePDF.csproj"
COPY . .
WORKDIR "/src/CreatePDF"
RUN dotnet build "CreatePDF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreatePDF.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreatePDF.dll"]