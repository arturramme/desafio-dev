# Desafio Programação

O projeto consiste em parsear um arquivo de texto (CNAB) pré-estabelecido e salvar suas informações - transações financeiras - em um banco de dados SQL Server. O usuário poderá importar o arquivo a partir de uma interface de upload. Realizado o upload, os dados importados são exibidos em tela. O software foi desenvolvido na linguagem C# com os frameworks Blazor e ASP.NET Web API Core na versão net6. 

## 🚀 Começando

O download do projeto pode ser realizado no link: git@github.com:arturramme/desafio-dev.git.

### 📋 Pré-requisitos

Para executar o projeto será necessário instalar o docker (+ docker compose). Para certificar que ambos foram instalados, você pode executar os comandos em um prompt de comando:

```
docker --version
docker-compose --version
```

### 🔧 Instalação

Com o docker instalado, é possível executar o projeto. Para isso rode o comando na raiz do projeto:

```
docker-compose up
```

O docker realizará as operações de download e configuração do projeto. O projeto pode ser visto na url: http://localhost:8080/

Para finalizar a execução:
```
docker-compose down
```

O projeto conta com dois métodos de API:
```
POST transaction/financial/cnab - inserção do cnab na base de dados
GET transactions/financial - consulta das transações financeiras agrupados por loja
```

## ⚙️ Executando os testes

O projeto conta com testes unitários automatizados. Para iniciar os testes, siga os passos abaixo:
```
1. Acessar a pasta "test"
2. Executar o comando: dotnet run
3. O resultado deve ser parecido com esse:
Aprovado!  – Com falha:     0, Aprovado:    10, Ignorado:     0, Total:    10, Duração: 67 ms - test.dll (net6.0)

Obs.: Necessário ter a versão net6 instalada.
```

## ✒️ Autores

* **Artur Ramme**
