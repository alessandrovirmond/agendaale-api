@echo off
echo ==========================================
echo Inicializando a Arquitetura do AgendaAle...
echo ==========================================

echo.
echo [1/4] Criando a Solution...
dotnet new sln -n AgendaAle

echo.
echo [2/4] Criando os Projetos...
dotnet new classlib -n AgendaAle.Domain
dotnet new classlib -n AgendaAle.Application
dotnet new classlib -n AgendaAle.Infrastructure
dotnet new webapi -n AgendaAle.Api
dotnet new worker -n AgendaAle.AnalyticsWorker

echo.
echo [3/4] Adicionando os Projetos na Solution...
dotnet sln add AgendaAle.Domain/AgendaAle.Domain.csproj
dotnet sln add AgendaAle.Application/AgendaAle.Application.csproj
dotnet sln add AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj
dotnet sln add AgendaAle.Api/AgendaAle.Api.csproj
dotnet sln add AgendaAle.AnalyticsWorker/AgendaAle.AnalyticsWorker.csproj

echo.
echo [4/4] Adicionando as Referencias de Arquitetura...
dotnet add AgendaAle.Application/AgendaAle.Application.csproj reference AgendaAle.Domain/AgendaAle.Domain.csproj
dotnet add AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj reference AgendaAle.Domain/AgendaAle.Domain.csproj
dotnet add AgendaAle.Api/AgendaAle.Api.csproj reference AgendaAle.Application/AgendaAle.Application.csproj
dotnet add AgendaAle.Api/AgendaAle.Api.csproj reference AgendaAle.Infrastructure/AgendaAle.Infrastructure.csproj
dotnet add AgendaAle.AnalyticsWorker/AgendaAle.AnalyticsWorker.csproj reference AgendaAle.Domain/AgendaAle.Domain.csproj

echo.
echo ==========================================
echo Tudo pronto! Estrutura criada com sucesso.
echo ==========================================
pause