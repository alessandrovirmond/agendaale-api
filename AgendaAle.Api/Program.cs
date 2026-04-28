using AgendaAle.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona os serviços da nossa camada de Infraestrutura (PostgreSQL, etc)
builder.Services.AddInfrastructure(builder.Configuration);

// 2. Habilita a arquitetura de Controllers (onde criaremos nossos endpoints reais)
builder.Services.AddControllers();

// 3. Mantém o OpenAPI moderno do seu template
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 4. Habilita a autorização para validar o token SSO depois
app.UseAuthorization();

// 5. Mapeia os nossos futuros Controllers
app.MapControllers();

app.Run();