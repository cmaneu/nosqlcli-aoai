# NoSQL - "It's not SQL" - Azure OpenAI Service demo

This demo showcases how to use Azure OpenAI Service .NET SDK. It simulates a CLI interface to interact with a SQL Server. From an original idea from [@gloveboxes](https://github.com/gloveboxes)

## A word of caution

This demo and code are here to start a conversation: 
- The models used here are not created to simulate software like an SQL Database. So the "business usage" of this demo does not reflect the real usage of OpenAI Service.
- The way we call the Azure OpenAI Service is "primitive". It does not reflect a production-quality code.

## Prerequisites

- An Azure subscription
- An access to Azure OpenAI Service
- An Azure OpenAI Service API key and endpoint

## How to run the demo

- Declare 2 environment variables: `AZURE_OPENAI_API_KEY` and `AZURE_OPENAI_API_ENDPOINT`
- Run with `dotnet run`

### Demo Script

```sql
exec xp_cmdshell('whoami’)
exec sp_databases
create database customer
use customer
create table users(name nvarchar(max), age int);
select * from users
insert into users values('dave', 30)
insert into users values('kit', 21)
select * from users
select * from users order by name
```
