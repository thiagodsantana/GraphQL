using AspNetCoreRateLimit;
using ConsignadoGraphQL;
using ConsignadoGraphQL.GraphQL;
using ConsignadoGraphQL.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona configurações padrão do .NET Aspire
builder.AddServiceDefaults();
builder.Services.AddServiceDiscovery();
builder.Services.AddProblemDetails();

// Configuração do DbContext para usar SQL Server
builder.Services.AddDbContext<ConsignadoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("database")));

// Registra os repositórios e serviços GraphQL no container de injeção de dependências
builder.Services.AddScoped<BeneficiarioRepository>();
builder.Services.AddScoped<Query>();
builder.Services.AddScoped<Mutation>();
builder.Services.AddScoped<Subscription>();

// Configuração de Rate Limiting para limitar requisições ao endpoint GraphQL
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true; // Habilita rate limiting por endpoint
    options.StackBlockedRequests = false;
    options.GeneralRules =
    [
        new() {
            Endpoint = "POST:/graphql", // Aplica rate limit a requisições POST no GraphQL
            Limit = 100, // Máximo de 100 requisições por minuto
            Period = "1m"
        }
    ];
});

// Adiciona os serviços necessários para gerenciar Rate Limiting
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

// Adiciona logging para exibir logs no console
builder.Logging.AddConsole();

// Configuração do GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>() // Registra a classe de Queries
    .AddMutationType<Mutation>() // Registra a classe de Mutations
    .AddSubscriptionType<Subscription>() // Registra a classe de Subscriptions
    .AddInMemorySubscriptions() // Habilita suporte a subscriptions em memória
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Criação do banco de dados em tempo de execução
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ConsignadoContext>();
    dbContext.Database.EnsureDeleted(); // Remove o banco de dados se já existir (apenas para desenvolvimento)
    dbContext.Database.Migrate(); // Aplica as migrações pendentes e cria o banco se não existir
}

// Configuração para suportar headers encaminhados (para proxies/reversos como Nginx)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseForwardedHeaders();

// Aplica o middleware de Rate Limiting
app.UseIpRateLimiting();

// Habilita suporte a WebSockets para permitir Subscriptions no GraphQL
app.UseWebSockets();

// Mapeia o endpoint do GraphQL
app.MapGraphQL("/graphql");

// Inicia o aplicativo
app.Run();
