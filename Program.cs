using AspNetCoreRateLimit;
using ConsignadoGraphQL.GraphQL;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddServiceDiscovery();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<Query>();
builder.Services.AddScoped<Mutation>();
builder.Services.AddScoped<Subscription>();

// 📌 Adicionando Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.GeneralRules =
    [
        new() {
            Endpoint = "POST:/graphql", // 🔥 Aplica o rate limit a requisição
            Limit = 100,      // 🔥 Máximo de 1 requisição por minuto
            Period = "1m"
        }
    ];
});

builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Logging.AddConsole();


// 📌 Adicionando GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions() // Habilita suporte a subscriptions em memória
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseForwardedHeaders();

// 📌 Aplicar middleware de Rate Limiting
app.UseIpRateLimiting();

// 📌 Habilitar suporte a WebSockets (necessário para subscriptions)
app.UseWebSockets();

app.MapGraphQL("/graphql");

app.Run();
