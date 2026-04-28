@echo off
echo ==========================================
echo Instalando Pacotes NuGet (Bibliotecas)...
echo ==========================================

echo.
echo [1/4] Configurando Application (Regras de Negocio)...
dotnet add AgendaAle.Application/AgendaAle.Application.csproj package MediatR
dotnet add AgendaAle.Application/AgendaAle.Application.csproj package FluentValidation.DependencyInjectionExtensions

echo.
echo [2/4] Configurando Infrastructure (Banco e Mensageria)...
dotnet add AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj package MassTransit.RabbitMQ

echo.
echo [3/4] Configurando a Api (Autenticacao e Migrations)...
dotnet add AgendaAle.Api/AgendaAle.Api.csproj package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add AgendaAle.Api/AgendaAle.Api.csproj package Microsoft.EntityFrameworkCore.Design

echo.
echo [4/4] Configurando AnalyticsWorker (Escuta de Filas)...
dotnet add AgendaAle.AnalyticsWorker/AgendaAle.AnalyticsWorker.csproj package MassTransit.RabbitMQ

echo.
echo ==========================================
echo Pacotes instalados com sucesso!
echo ==========================================
pause