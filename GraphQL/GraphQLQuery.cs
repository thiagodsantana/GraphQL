using System.Text.Json.Serialization;

namespace ConsignadoGraphQL.GraphQL
{
    public class GraphQLQuery
    {
        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;
    }
}