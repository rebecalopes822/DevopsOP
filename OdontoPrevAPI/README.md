# OdontoPrev API

API desenvolvida em **ASP.NET Core Web API** para gerenciamento de pacientes, tratamentos e sinistros odontológicos.

## Integrantes do Grupo

- **Giovanna Lima** - RM: RM553369  
- **Felipe Arcanjo** - RM: RM554018  
- **Rebeca Lopes** - RM: RM553764  

## Arquitetura do Projeto

Optamos por uma **arquitetura monolítica**, adequada ao escopo da solução. Em caso de expansão, pode ser migrada para uma arquitetura de **microservices**.

## Design Patterns Utilizados

- **Singleton** → Classe `ConfigManager` para centralizar a string de conexão.
- **Repository Pattern** → Separação da lógica de acesso a dados com `PacienteRepository`, `TratamentoRepository` e `SinistroRepository`.

## Práticas de Clean Code Aplicadas

- **Separação de responsabilidades**: Controllers, DTOs, Services e Repositories organizados por domínio.
- **Nomenclatura significativa**: métodos e variáveis com nomes autoexplicativos.
- **Validações claras**: todos os endpoints aplicam validações de entrada para garantir integridade de dados.
- **Injeção de dependência**: via `services.AddScoped`, promovendo desacoplamento e testabilidade.

### Integração com API Externa (ViaCEP)

A API ViaCEP é utilizada para buscar automaticamente os dados de endereço com base no CEP informado, melhorando a qualidade dos dados e reduzindo erros de digitação.

### Funcionalidade de IA Generativa

A aplicação prevê a **complexidade do tratamento odontológico** com base em 3 parâmetros:

- Tipo de tratamento  
- Idade do paciente  
- Se está sintomático ou não  

A IA foi treinada com um conjunto de 500 amostras e integrada via ML.NET.

- **Finalidade**: auxiliar na triagem e classificação clínica dos atendimentos, reduzindo a chance de sinistros por má categorização.

Exemplo de requisição para a IA:

```json
{
  "tipoTratamento": "Ortodontia",
  "idade": 35,
  "sintomatico": "Sim"
}
```

Resposta esperada:

```json
{
  "complexidadePrevista": "Alta"
}
```

## Testes Implementados

- **PacienteRepositoryTests**:
  - Testa o método de adicionar pacientes com banco de dados em memória.
- **ViaCepServiceTests**:
  - Simula chamadas HTTP e valida a resposta da API externa (ViaCEP).

Ambos os testes cobrem os principais fluxos críticos, com mocks e asserts claros.

## Como Rodar a API

### Clonar e Executar

1. **Clone o projeto** no Visual Studio 2022.  
2. Defina o projeto API como projeto de inicialização.  
3. Execute `dotnet restore` no terminal.  
4. Pressione `F5` ou use `dotnet run`.

Acesse o Swagger via:

```
http://localhost:5000/swagger
```

## Tecnologias Utilizadas

- ASP.NET Core 8  
- Entity Framework Core  
- Oracle Database  
- ML.NET  
- Swagger / OpenAPI  
- xUnit / Moq  
- Postman (testes manuais)