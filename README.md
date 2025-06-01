# 📦 Importador de Contratos - Desafio Concilig

Este projeto é uma API em ASP.NET Core 8.0 que permite a **importação de arquivos CSV** com dados de contratos, associando-os a usuários autenticados. Conta com:

* ✅ Autenticação JWT
* ✅ Upload de CSV com validação
* ✅ Salvamento no banco de dados SQL Server
* ✅ Consultas dos contratos, usuários e arquivos
* ✅ Documentação via Swagger com suporte a OpenAPI 3.1

---

## 🚀 Tecnologias Utilizadas

* ASP.NET Core 8.0
* Entity Framework Core
* SQL Server
* JWT (Json Web Token)
* Swagger / OpenAPI 3.1.0
* CsvHelper
* Visual Studio 2022

---

## ⚙️ Pré-requisitos

* .NET SDK 8.0+
* SQL Server (Express ou LocalDB)
* Visual Studio 2022 ou VS Code
* Postman ou Swagger (para testes)
* Git

---

## 🛠️ Instalação do SQL Server

1. Baixe e instale o **SQL Server Express**:
   [https://www.microsoft.com/pt-br/sql-server/sql-server-downloads](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

2. Instale o **SQL Server Management Studio (SSMS)**:
   [https://learn.microsoft.com/pt-br/sql/ssms/download-ssms](https://learn.microsoft.com/pt-br/sql/ssms/download-ssms)

3. Crie o banco de dados com o seguinte comando:

```sql
CREATE DATABASE BdDesafioConcilig;
```

---

## 🧱 Estrutura das Tabelas

```sql
USE BdDesafioConcilig;

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE ArquivosImportados (
    Id INT PRIMARY KEY IDENTITY,
    NomeArquivo NVARCHAR(255),
    DataImportacao DATETIME,
    UsuarioId INT,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

CREATE TABLE Contratos (
    Id INT PRIMARY KEY IDENTITY,
    NomeCliente NVARCHAR(100),
    CPF NVARCHAR(20),
    NumeroContrato NVARCHAR(50),
    Produto NVARCHAR(100),
    DataVencimento DATE,
    Valor DECIMAL(18, 2),
    ArquivoImportadoId INT,
    UsuarioId INT,
    DataImportacao DATETIME,
    FOREIGN KEY (ArquivoImportadoId) REFERENCES ArquivosImportados(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
```

---

## 👤 Usuário de Teste

Insira um usuário para autenticar:

```sql
INSERT INTO Usuarios (Nome, Email)
VALUES ('Admin Teste', 'admin@teste.com');
```

---

## ▶️ Como Executar o Projeto

1. Clone o repositório:

```bash
git clone https://github.com/TheoRondon25/DesafioConcilig_ImportacaoContratos.git
cd DesafioConcilig_ImportacaoContratos
```

2. Configure o `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BdDesafioConcilig;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. Execute o projeto:

```bash
dotnet run
```

4. Acesse no navegador:

```
https://localhost:{porta}/swagger
```
A porta irá aparecer no terminal após a execução.

---

## 📄 Testando o Upload

1. Autentique-se com:

```http
POST /api/auth/login
```

```json
{
  "email": "admin@teste.com"
}
```

2. Copie o token JWT retornado.

3. Use o token no Swagger (clicando em "Authorize") ou Postman.

4. Faça upload via:

```http
POST /api/importacao/upload
```

* Tipo de arquivo: `.csv`
* Codificação: `Windows-1252`
* Delimitador: `;`

Exemplo de conteúdo:

```
Nome;CPF;Contrato;Produto;Vencimento;Valor
Maria;12345678900;ABC123;Produto A;12/05/2022;1599,99
```

---

## 🔍 Endpoints de Consulta

| Método | Endpoint                             | Descrição                                               |
| ------ | ------------------------------------ | ------------------------------------------------------- |
| GET    | `/api/consultas/arquivos-importados` | Lista arquivos importados e quem os importou            |
| GET    | `/api/consultas/contratos`           | Lista todos os contratos importados                     |
| GET    | `/api/consultas/clientes/analise`    | Total de contratos e maior atraso por cliente (em dias) |

---

## ✅ Diferenciais

* Autenticação com JWT e validação de token
* Tratamento de CSV com acentos e datas brasileiras
* Mensagens de erro claras
* Endpoints protegidos
* Documentação descritiva no Swagger

---

## 🎯 Melhorias Futuras

* Cadastro de usuários com senha
* Dashboard com gráficos e filtros
* Deploy com Docker e CI/CD
* Exportação de relatórios em PDF ou Excel

---

## 👨‍💼 Autor

**Theo Rondon**
🔗 GitHub: [@TheoRondon25](https://github.com/TheoRondon25)

---

## 📄 Licença

Este projeto está licenciado sob os termos da licença MIT.

Copyright (c) 2025 Theo Rondon
