# 📚 Biblioteca API

API REST para gerenciamento de uma biblioteca, desenvolvida em C# com ASP.NET Core. O projeto tem como objetivo praticar o desenvolvimento de APIs utilizando uma arquitetura organizada em camadas, separação de responsabilidades, autenticação, persistência de dados e tratamento centralizado de erros.

## 🚀 Tecnologias utilizadas

* **C# / .NET** — linguagem e plataforma de desenvolvimento.
* **ASP.NET Core Web API** — criação dos endpoints REST.
* **Entity Framework Core** — acesso e persistência de dados.
* **MySQL** — banco de dados relacional.
* **MediatR** — implementação do padrão Mediator para separar controllers e operações da aplicação.
* **JWT (JSON Web Token)** — autenticação baseada em tokens.
* **AutoMapper** — mapeamento entre objetos.
* **Swagger** — documentação e testes dos endpoints.
* **Middleware personalizado** — tratamento global de exceções e logging de requisições.

## 🏗️ Arquitetura

O projeto utiliza uma arquitetura organizada em camadas, buscando manter as responsabilidades separadas e facilitar a manutenção e evolução da aplicação.

```text
Biblioteca/
├── Biblioteca.Api/
│   ├── Controllers/
│   ├── Middleware/
│   └── Program.cs
│
├── Biblioteca.Application/
│   └── Modules/
│       ├── LivroOps/
│       ├── UsuarioOps/
│       ├── CategoriaOps/
│       └── EmprestimoOps/
│
├── Biblioteca.Domain/
│   ├── _Core/
│   │   └── ExceptionBase/
│   └── Models/
│
└── Biblioteca.Infrastructure/
    └── Configurações de infraestrutura
```

*Observação: a estrutura acima é ilustrativa e pode variar conforme a organização atual dos arquivos no repositório.*

### Responsabilidades das camadas

* **API:** recebe as requisições HTTP, aplica as regras de acesso aos endpoints e retorna as respostas.
* **Application:** concentra os comandos e as consultas da aplicação, utilizando MediatR para encaminhar as operações.
* **Domain:** reúne os conceitos de negócio e as exceções utilizadas pela aplicação.
* **Infrastructure:** concentra as implementações relacionadas à infraestrutura, incluindo o acesso ao banco de dados.

## ✨ Funcionalidades

### 📖 Gerenciamento de livros

* Cadastro de livros.
* Consulta de livros.
* Consulta de livro por ID.
* Atualização de livros.
* Exclusão de livros.
* Associação de livros a categorias.
* Controle de disponibilidade para empréstimos.

### 🗂️ Gerenciamento de categorias

* Cadastro de categorias.
* Consulta de categorias.
* Consulta de categoria por ID.
* Atualização e exclusão de categorias.
* Tratamento de conflitos relacionados a livros associados.

### 👤 Gerenciamento de usuários

* Cadastro de usuários.
* Consulta do próprio perfil.
* Consulta administrativa de usuários.
* Atualização e exclusão de usuários.
* Validação de e-mail e funcionalidades de confirmação por código.

### 🔐 Autenticação e autorização

* Login de usuários.
* Autenticação utilizando JWT.
* Controle de acesso baseado em funções (*roles*).
* Restrição de operações administrativas à role `Administrador`.

### 📕 Gerenciamento de empréstimos

* Criação de empréstimos.
* Devolução de livros.
* Consulta de empréstimo por ID.
* Consulta administrativa de empréstimos.
* Consulta dos empréstimos do usuário autenticado.
* Tratamento de situações como livro indisponível e empréstimo já devolvido.

## 🛡️ Tratamento global de exceções

A API possui um middleware personalizado para centralizar o tratamento de exceções e padronizar as respostas de erro.

Entre os status HTTP contemplados estão:

| Status | Significado             |
| ------ | ----------------------- |
| `400`  | Requisição inválida     |
| `401`  | Não autenticado         |
| `403`  | Acesso proibido         |
| `404`  | Recurso não encontrado  |
| `409`  | Conflito de negócio     |
| `500`  | Erro interno inesperado |

As respostas de exceção seguem uma estrutura JSON padronizada:

```json
{
  "status": 409,
  "message": "Livro não está disponível.",
  "timestamp": "2026-10-14T12:00:00+00:00"
}
```

O timestamp é ilustrativo. Exceções inesperadas retornam uma mensagem genérica, evitando expor detalhes internos da aplicação.

## 📋 Logging de requisições

A API também possui um middleware de logging que registra informações sobre o ciclo de vida das requisições HTTP, incluindo:

* Método HTTP.
* Caminho requisitado.
* Status code da resposta.
* Tempo de processamento em milissegundos.

Exemplo ilustrativo:

```text
Requisição iniciada: GET /api/livros
Requisição finalizada: GET /api/livros | Status: 200 | Duração: 42 ms
```

O objetivo é facilitar o acompanhamento das requisições e auxiliar na identificação de problemas durante o desenvolvimento.

## ⚙️ Como executar o projeto

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download)
* [MySQL](https://www.mysql.com/)
* [Git](https://git-scm.com/)

### 1. Clonar o repositório

```bash
git clone https://github.com/FelipeMovio/API-Gerenciamento-Biblioteca.git
```

Entre na pasta do projeto:

```bash
cd API-Gerenciamento-Biblioteca
```

### 2. Configurar o banco de dados

Crie um banco de dados MySQL para a aplicação.

Configure a connection string de acordo com a configuração utilizada pelo projeto. Por exemplo, no arquivo `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "BibliotecaConnection": "Server=localhost;Database=bibliotecadb;User=SEU_USUARIO;Password=SUA_SENHA;"
  }
}
```

Os nomes das configurações devem corresponder aos utilizados pela aplicação.

**Importante:** não compartilhe senhas ou chaves JWT no repositório. Utilize User Secrets ou variáveis de ambiente para informações sensíveis.

### 3. Configurar o JWT

Configure os valores necessários para autenticação, como chave de assinatura, emissor e audiência:

```json
{
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA",
    "Issuer": "SEU_EMISSOR",
    "Audience": "SUA_AUDIENCIA"
  }
}
```

Use valores apropriados para o seu ambiente. A chave acima é apenas um exemplo de configuração, não uma chave pronta para produção.

### 4. Aplicar as migrations

Com a ferramenta do Entity Framework Core instalada e a configuração do banco definida, aplique as migrations existentes.

A partir da raiz da solução, ajuste os caminhos conforme a localização real dos projetos:

```bash
dotnet ef database update --project Biblioteca.Infrastructure --startup-project Biblioteca.Api
```

### 5. Executar a API

Execute o projeto da API:

```bash
dotnet run --project Biblioteca.Api
```

O endereço local será exibido no terminal. Se o Swagger estiver habilitado no ambiente de execução, acesse a rota `/swagger` no endereço informado.

## 🧪 Testando a API

Com a aplicação em execução, utilize o Swagger para explorar os endpoints, enviar requisições e conferir as respostas HTTP.

Alguns exemplos de rotas:

| Método   | Rota                             | Operação                 |
| -------- | -------------------------------- | ------------------------ |
| `GET`    | `/api/livros`                    | Listar livros            |
| `GET`    | `/api/livros/{id}`               | Consultar livro          |
| `POST`   | `/api/livros`                    | Cadastrar livro          |
| `PUT`    | `/api/livros/{id}`               | Atualizar livro          |
| `DELETE` | `/api/livros/{id}`               | Excluir livro            |
| `GET`    | `/api/categoria`                 | Listar categorias        |
| `POST`   | `/api/emprestimos`               | Criar empréstimo         |
| `PUT`    | `/api/emprestimos/{id}/devolver` | Devolver empréstimo      |
| `GET`    | `/api/usuario/me`                | Consultar próprio perfil |
| `POST`   | `/api/auth/login`                | Autenticar usuário       |

As rotas administrativas exigem as permissões correspondentes. A lista é ilustrativa e deve ser conferida com os controllers atuais.

## 🎯 Objetivos de aprendizado

Este projeto faz parte da minha evolução no desenvolvimento backend com C# e .NET, com foco em:

* Desenvolvimento de APIs REST.
* Organização de aplicações em camadas.
* Separação de responsabilidades.
* Padrão Mediator com MediatR.
* Persistência de dados com Entity Framework Core.
* Autenticação e autorização com JWT.
* Tratamento global de exceções.
* Criação e registro de middlewares personalizados.
* Versionamento de código com Git e organização do desenvolvimento por branches.

## 📌 Status do projeto

Projeto em desenvolvimento contínuo, com funcionalidades implementadas por etapas para consolidar conhecimentos de arquitetura, desenvolvimento de APIs e boas práticas com .NET.

## 👨‍💻 Autor

**Felipe Lopes dos Santos Movio**

* GitHub: [FelipeMovio](https://github.com/FelipeMovio)
