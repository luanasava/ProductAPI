# API de Produto 

## Resumo

Este projeto consiste em uma API Web para o gerenciamento de produtos, com funcionalidades como cadastro, atualização, exclusão, consulta e listagem de produtos. A API foi desenvolvida utilizando .NET 6.0 e Entity Framework com MySQL. Além disso, foram implementados testes unitários utilizando **Moq** e **AutoMapper**, e integração contínua usando GitHub Actions.


## A entidade `Produto`
Propriedades:

- **Id**: Identificador único do produto.
- **Nome**: Nome do produto.
- **Estoque**: Quantidade disponível em estoque.
- **Valor**: Preço do produto.

## Funcionalidades

### 1. **Criar um Produto**
- Endpoint: `POST /api/produtos`
- Descrição: Permite o cadastro de um novo produto.
- Requisitos:
  - O valor do produto não pode ser negativo.

### 2. **Atualizar um Produto**
- Endpoint: `PUT /api/produtos/{id}`
- Descrição: Permite a atualização dos dados de um produto existente.

### 3. **Deletar um Produto**
- Endpoint: `DELETE /api/produtos/{id}`
- Descrição: Remove um produto do sistema.

### 4. **Listar Produtos**
- Endpoint: `GET /api/produtos`
- Descrição: Retorna a lista de produtos cadastrados.
- Funcionalidades:
  - Ordenar os produtos por diferentes campos.
  - Buscar produto pelo nome.

### 5. **Consultar um Produto Específico (Id ou Nome)**
- Endpoint: `GET /api/produtos/{id}`
- Endpoint: `GET /api/produtos/{nome}`
- Descrição: Retorna os dados de um produto específico.
- Funcionalidades:
  - Buscar produto pelo identificador.
  - Buscar produto pelo nome.

## Tecnologias Utilizadas

- **.NET 6**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM para comunicação com o banco de dados MySQL.
- **AutoMapper**: Usado para mapear as entidades de domínio para DTOs (Data Transfer Objects), facilitando a transformação de dados entre camadas e tornando o código mais limpo.
- **MySQL**: Banco de dados utilizado para persistência de dados.
- **Moq**: Framework utilizado para criar mocks e simular comportamentos de dependências durante os testes unitários, garantindo o isolamento de testes.
- **GitHub Actions**: Para integração contínua e execução de testes de integração.


## Persistência de Dados

A persistência de dados neste projeto é feita utilizando o **MySQL**, um banco de dados relacional. O **Entity Framework Core** foi utilizado como ORM (Object-Relational Mapper) para mapear as entidades para o banco de dados. A abordagem **Code-First** foi escolhida, o que significa que as classes de modelo (como a entidade `Produto`) são definidas primeiro no código, e o **Entity Framework** gera automaticamente o banco de dados e as migrações a partir dessas classes.

- **Banco de dados**: **MySQL**
- **ORM utilizado**: **Entity Framework Core**
- **Abordagem**: **Code-First** (as entidades são modeladas no código e, a partir delas, são geradas as tabelas no banco de dados)
- **Migrações**: As migrações do **Entity Framework** são usadas para criar e atualizar o banco de dados, permitindo o versionamento da estrutura de dados.

### Seed Data
- Ao iniciar a aplicação, um conjunto de 5 produtos é inserido automaticamente no banco de dados através do **Seed Data**. Isso garante que, ao rodar a aplicação pela primeira vez, os dados básicos já estarão disponíveis para testes e para o funcionamento da API.
- Além disso, produtos de teste são adicionados automaticamente durante o desenvolvimento da API para garantir que as funcionalidades de CRUD funcionem conforme esperado.


## AutoMapper

Foi utilizado o **AutoMapper** para facilitar o mapeamento de dados entre as entidades de domínio (modelos) e os **DTOs** (objetos de transferência de dados). O AutoMapper ajuda a criar um código mais limpo e facilita a manutenção, já que a transformação dos dados entre diferentes camadas da aplicação é feita de forma centralizada.

## Testes

### Testes Unitários
- Todos os endpoints da API foram testados utilizando **xUnit**.
- A framework **Moq** foi utilizada para criar mocks de dependências, garantindo que os testes unitários possam ser realizados de forma isolada e eficiente.
- O **AutoMapper** foi configurado para garantir que a transformação de dados entre as camadas do sistema esteja funcionando corretamente.

### Testes de Integração
- Foi configurado o teste de integração no **GitHub Actions**, executando a API em um ambiente isolado para garantir a qualidade e a estabilidade do sistema durante as alterações no código.


## Como Executar

### Pré-requisitos
- **.NET 6.0** instalado em seu ambiente.
- **MySQL** instalado e configurado.

### Executando Localmente

1. Clone o repositório:
   ```bash
   git clone https://github.com/seuusuario/ProdutoAPI.git
   cd ProdutoAPI
