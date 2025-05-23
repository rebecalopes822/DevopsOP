
# OdontoDevops4

Este repositório faz parte da entrega da Sprint 4 da disciplina de DevOps, utilizando Azure DevOps para configurar uma pipeline CI/CD com base em uma aplicação ASP.NET 8.

## Sobre a aplicação

A aplicação é uma API desenvolvida em .NET 8 voltada para o gerenciamento de pacientes, tratamentos odontológicos e previsão de risco de sinistro com IA. Os dados são armazenados em um banco Oracle e a aplicação segue boas práticas de Clean Code, com testes automatizados.

## Pipeline CI/CD

A pipeline foi criada com Azure DevOps, utilizando YAML para definir as etapas de build e publicação de artefatos.

### Etapas principais:

- Trigger ao fazer push na branch `main`
- Ambiente com `windows-latest`
- Instalação do .NET SDK 8
- Restore de pacotes NuGet
- Build da aplicação (.csproj)
- Geração dos artefatos (.dll, .json etc.)
- Publicação dos artefatos como `drop`

## Como executar a aplicação

### Opção 1: Usando Visual Studio

1. Clone o repositório
2. Abra a solução `.sln` no Visual Studio
3. Configure a conexão com o banco Oracle no `appsettings.json`
4. Compile e execute com `F5`

### Opção 2: Usando terminal

1. Clone o repositório
2. No terminal, vá até a pasta do projeto e rode:
   ```
   dotnet restore
   dotnet build -c Release
   dotnet run
   ```

## Testando o CRUD via JSON

Você pode usar ferramentas como o Postman para testar os endpoints da API com os seguintes exemplos:

### Criar paciente (`POST /api/pacientes`)

```json
{
  "id": 0,
  "nome": "Beatriz Lima",
  "email": "beatriz.lima@email.com",
  "dataNascimento": "2000-11-30T00:00:00",
  "telefone": "11966554433",
  "generoId": 2,
  "enderecoId": 4
}
```

### Criar tratamento (`POST /api/tratamentos`)

```json
{
  "id": 0,
  "descricao": "Clareamento dental a laser",
  "tipo": "Estético",
  "custo": 600.00
}
```

### Criar relação paciente-tratamento (`POST /api/paciente-tratamentos`)

```json
{
  "id": 0,
  "pacienteId": 17,
  "tratamentoId": 9,
  "dataOcorrencia": "2025-05-18T09:30:00",
  "status": "Aprovado"
}
```

## Executando a pipeline

1. Faça um push na branch `main`
2. Acesse o Azure DevOps e vá até o projeto `OdontoDevops4`
3. Na aba `Pipelines`, acompanhe a execução
4. Ao final, vá em `Artifacts > drop` para baixar os arquivos gerados

## Links

- [Repositório no GitHub](https://github.com/rebecalopes822/DevopsOP.git)
- [Azure DevOps - Pipeline](https://dev.azure.com/VikezeCH/Odontoprev%20-%20Rebeca)

---

## Integrante:

- **Giovanna Lima** - RM: RM553369  
- **Felipe Arcanjo** - RM: RM554018  
- **Rebeca Lopes** - RM: RM553764 
