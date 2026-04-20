# AgendaAle API

Backend responsável pelo processamento de dados, autenticação SSO e sincronização de tarefas do aplicativo mobile AgendaAle. A arquitetura utiliza filas de mensageria para separar a carga de escrita no banco de dados do processamento de relatórios, suportando perfeitamente o fluxo de sincronização em lote de clientes que estavam offline.

## 🚀 Tecnologias

* **Linguagem/Framework:** C# / .NET 8 (Web API & Worker Services)
* **Banco de Dados:** PostgreSQL
* **Mensageria:** RabbitMQ
* **Infraestrutura:** Docker & Docker Compose
* **ORM:** Entity Framework Core

## 🎯 Objetivo Inicial: Módulo de Login (SSO)

O primeiro módulo desenvolvido trata a autenticação segura do usuário:
1. Recebe o Token JWT gerado no client-side.
2. Valida a integridade do token.
3. Verifica a existência do usuário no PostgreSQL. Se for o primeiro acesso, cria um registro atrelando o e-mail a um ID único para a criação isolada de suas agendas.

## 🛠️ Como rodar localmente

1. Clone o repositório.
2. Suba a infraestrutura base com Docker:
   `docker-compose up -d` (inicia o PostgreSQL e o RabbitMQ).
3. Configure sua string de conexão no `appsettings.Development.json`.
4. Execute as migrations do Entity Framework para criar as tabelas iniciais.
5. Inicie o projeto via CLI (`dotnet run`).