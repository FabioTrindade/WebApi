# WebApi
Desafio Dev. Backend Senior

<h4 align="center"> 
	:construction:  Web Api :rocket: Em constante desenvolvimento...  :construction:
</h4>

# Descrição do Projeto
API de venda ecommerce, realiza gestão de tipo de pagamento, tipo de status, cadastro de produto com controle de estoque e venda.

## :books: Design Patner
- DDD (Domain Drive Design)
- SOLID


# :page_with_curl: Estrutura

A divisão das responsabilidades estão separadas por pastas


#### Commons
- Arquivos comum a todo o projeto


#### Configurations
- Configurações do projeto


#### Controllers
- Porta de entrada do projeto, onde estão centralizado as rotas


#### Domain
- Classes abstratas
- Command (Responsável por receber os parametros enviados do front)
- DTO (Responsável na transferência de dados entres nosso serviço e aplicação)
- Entities (Representatividade de nosso modelo de dados)
- Enums (Valores abstratos pré definidos)
- Providers (Abstrações de provedores externos)
- Repositories (Abstrações que faz a mediação entre o domínio e as camadas de mapeamento de dados)
- Responses (Modelo de representatividade das ações)
- Services (Abstração de nossos serviços)


#### Exntensions
- Destinados a métodos auxiliares, bem como extensões entre outras aplicabilidade.

#### Filters
- utilizado para definir filtros comuns a toda a aplicação


#### Infra
- Contexts (Realiza conexão de nosso modelo de dados à database)
- Mappings (Customização de atributos e modelo de representatividade de dados, bem como definição de respectivas chaves (primaria e estrangeira))
- Repositories (Implementação das definições realizadas na camada Domain)


#### Middlewares
- Incumbido para manipular as requisições e respostas, além de capturar exceções que ocorrem na aplicação.


#### Migrations
- Automatizar o processo de geração e atualização do modelo de dados com base no modelo de entidades (Domain).


#### Services
- Providers (Implementação dos provedores definidos na camada Domain)
- Services (Implementação dos serviços definidos na camada Domain)


# :hammer_and_wrench: Tecnologias

- [<img align="center" alt=".NET 5" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg">.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [<img align="center" alt="Postgre SQL" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/postgresql/postgresql-original.svg"> PostgreSQL](https://www.postgresql.org/)
- [<img align="center" alt="EF Core" height="30" width="30" src="https://api.nuget.org/v3-flatcontainer/microsoft.entityframeworkcore/6.0.0-rc.2.21480.5/icon"> EF Core 5](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx)
- [<img align="center" alt="Dapper" height="20" width="25" src="https://api.nuget.org/v3-flatcontainer/dapper/2.0.90/icon"> Dapper](https://dapper-tutorial.net/)


# :hammer_and_pick: Ferramentas

- [<img align="center" alt="Visual Studio" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/visualstudio/visualstudio-plain.svg"> VisualStudio 2019](https://visualstudio.microsoft.com/vs/)
- [<img align="center" alt="DBeaver" height="30" width="30" src="https://dbeaver.io/wp-content/uploads/2015/09/beaver-head.png"> DBeaver](https://dbeaver.io/)


# :star: Contribuidores
<table>
<tr>
<td align="center">
<a href="https://github.com/FabioTrindade"><img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/30089341?v=4" width="100px;" alt="Fábio Trindade"/><br /><b>Fábio Trindade</b></a>
</td>
</tr>
</table>
