# Etapa 1: Build usando o SDK do .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /App

# Copia os arquivos de projeto e restaura dependências
COPY . ./
RUN dotnet restore

# Compila a versão final otimizada
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime usando a imagem mínima do ASP.NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /App
COPY --from=build-env /App/out .

# Expõe a porta que o Render exige
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando para ligar a API (Substitua pelo nome exato da sua DLL)
ENTRYPOINT ["dotnet", "AgendaAle.Api.dll"]