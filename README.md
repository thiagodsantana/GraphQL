# GraphQL API com HotChocolate e .NET

Este projeto implementa uma API GraphQL usando **HotChocolate** em .NET para gerenciar beneficiários, benefícios e contratos.

## 🚀 Tecnologias Utilizadas
- **.NET 7/8**
- **HotChocolate** (Biblioteca GraphQL para .NET)
- **In-Memory Repository** (Simulação de banco de dados)
- **Aspire** (Orquestração de aplicações distribuídas)

---

## 📌 Configuração

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-repositorio.git
   cd seu-repositorio
   ```
2. Instale as dependências necessárias:
   ```bash
   dotnet restore
   ```
3. Execute o projeto:
   ```bash
   dotnet run
   ```
4. Acesse o **Banana Cake Pop** (Interface GraphQL) em:
   ```
   http://localhost:5000/graphql
   ```

---

## 📡 Exemplo de Query (Consultar Beneficiários)

```graphql
query{
    beneficiarios{
        cpf
        nome,        
        beneficios{
            tipo,
            valor,
            contratos{
                parcelas,
                valorTotal,
                taxaJuros
            }
        }
    }
}
```

### 🔹 Resposta Esperada
```json
{
  "data": {
    "beneficiarios": [
      {
        "cpf": "12345678901",
        "nome": "João Silva",
        "beneficios": [
          {
            "tipo": "Aposentadoria",
            "valor": 3000,
            "contratos": [
              {
                "parcelas": 24,
                "valorTotal": 15000,
                "taxaJuros": 2.5
              }
            ]
          }
        ]
      }
    ]
  }
}
```

---

## 📝 Exemplo de Mutation (Adicionar Beneficiário)

```graphql
mutation {
  addBeneficiario(input: {
    nome: "Carlos Mendes", 
    cpf: "12345678910",
    beneficios: [
      {
        tipo: "Aposentadoria",
        valor: 4000,
        contratos: [
          { valorTotal: 20000, parcelas: 48, taxaJuros: 2.8 }
        ]
      }
    ]
  }) {
    id
    nome
    cpf
    beneficios {
      id
      tipo
      valor
      contratos {
        id
        valorTotal
        parcelas
        taxaJuros
      }
    }
  }
}
```

### 🔹 Resposta Esperada
```json
{
  "data": {
    "addBeneficiario": {
      "id": 3,
      "nome": "Carlos Mendes",
      "cpf": "12345678910",
      "beneficios": [
        {
          "id": 5,
          "tipo": "Aposentadoria",
          "valor": 4000,
          "contratos": [
            {
              "id": 7,
              "valorTotal": 20000,
              "parcelas": 48,
              "taxaJuros": 2.8
            }
          ]
        }
      ]
    }
  }
}
```

## 📝 Exemplo de Consumo Subscription

```subscription {
  onBeneficiarioAdded {
    id
    nome
    cpf
  }
}

```

### 🔹 Resposta Esperada
```{
  "data": {
    "onBeneficiarioAdded": {
      "id": 4,
      "nome": "Carlos Mendes",
      "cpf": "12345678910"
    }
  }
}
```

