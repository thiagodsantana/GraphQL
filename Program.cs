using ConsignadoGraphQL.GraphQL;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();
// Adiciona descoberta de servi�os
builder.Services.AddServiceDiscovery();
builder.Services.AddProblemDetails();

builder.Services.AddGraphQLServer().AddQueryType<Query>()
                                   .AddProjections()
                                   .AddFiltering()
                                   .AddSorting();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();