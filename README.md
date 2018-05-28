# PaymentGateway

PaymentGateway is a simple implementation of an API to process store sales, run them through an anti-fraud system if necessary and finally request the transactions with a credit card operator. 
These transactions are mapped to domain objects and parsed to the format expected by the credit card operators and anti fraud systems.

__Observations:__

- To persist the transaction, store, person and other relevant information, a local SQL Server database was used;
- The API currently implements the parsing, requesting and mocking of two credit card Operators, Cielo and Stone;
- The transaction requests are not actually sent to the operators and anti-fraud system's APIs, their responses are mocked;
- Only the mandatory fields of the requests were implemented on the API;
- The anti-fraud system accepts more than one transaction per request. For simplicity the API assumes that there is only one transaction on each request. This could be easily extended on a future version.


## Solution Structure

The solution contains three projects. A simple console application to run the solution, a class library project and a test project to run the unit tests.
To integrate this solution with another project, the important part is the class library "PaymentGateway.Model". This project is divided in four modules:

- Business: Module with the business logic, such as request mapping, transaction building and response mocking;
- Entity: Contains all domain classes, the building blocks of the project;
- DAL: The data access layer with the database and the classes to interact with it;
- Repository: Has the objects that abstract the interactions with the database. Implementing a repository, the business logic is completely unaware of how the data is being stored, this pattern is data structure agnostic.


## Database Structure

A simple database structure was implemented to store the important information to process the transactions. The database is a local SQL Server and is comprised of six tables as follows:

- Person: General person information, can be a store owner or a client;
- Store: Information about the store. Each Store has a Person owner, provided by a foreign key;
- CreditCard: Credit card information (foreign key to Person);
- Operator: Information about the credit card operators;
- AssociationStoreOperator: Connects the stores to the operators (foreign keys to both tables);
- AssociationStoreAntiFraud: Holds the login info of the stores on the anti-fraud system (foreign key to Store)
- Transaction: Table with all transactions processed by the API, even the ones denied by the operator or anti-fraud system,


## Getting Started

This project runs mostly on the standard .NET Core package, with a few additional libraries. Newtonsoft.Json is used to parse json strings and System.Data.SqlClient is used to connect to the database.

To run the application the following specifications must be matched:

- System compatible with .NET Core 2 with Microsoft SDK installed;
- Microsoft SQL Server LocalDB.

### Building

To build PaymentGateway run on the root folder:

```
dotnet build PaymentGateway.sln
```

### Testing

The solution provides a test project with a few of the important unit test cases on three core components of the application. The business logic to process and map requests, the mocking and processing of responses and the CRUD operations on the local database.
For simplicity, these tests are performed on the same database that stores real data. Ideally there should be a test database to run the tests.

To run all tests of tests run:

```
dotnet test PaymentGateway.Test.csproj
```

__Results:__
```
Build iniciada, aguarde...
Build concluído.

Execução de teste para D:\Documentos\Programas\PaymentGateway\PaymentGateway.Test\bin\Debug\netcoreapp2.0\PaymentGateway.Test.dll(.NETCoreApp,Version=v2.0)
Ferramenta de Linha de Comando de Execução de Teste da Microsoft (R) Versão 15.6.0
Copyright (c) Microsoft Corporation. Todos os direitos reservados.

Iniciando execução de teste, espere...
[xUnit.net 00:00:00.4373543]   Discovering: PaymentGateway.Test
[xUnit.net 00:00:00.4959585]   Discovered:  PaymentGateway.Test
[xUnit.net 00:00:00.5026548]   Starting:    PaymentGateway.Test
[xUnit.net 00:00:01.9359184]   Finished:    PaymentGateway.Test

Total de testes: 13. Aprovados: 13. Com falha: 0. Ignorados: 0.
Execução de Teste Bem-sucedida.
Tempo de execução de teste: 2.9254 Segundos
```

## API contract and Example

To process a transaction, the consumer must provide the following information to the Gateway API:

- Items being purchased;
- Number of installments of the payment;
- The credit card used to perform the transaction;
- The index of the credit card operator in the Store's Operator collection.

__Example:__
```
var idStore = 1
var buyerCardNumber = "123456"
var nInstallments = 1
var operatorIndex = 1

var gate = new Gateway(idStore);
var card = PersonRepository.GetCard(buyerCardNumber);

var items = new AntiFraud.Item[]
{
	new AntiFraud.Item("1", "Item1", 10, 2),
	new AntiFraud.Item("2", "Item2", 20, 3),
	new AntiFraud.Item("3", "Item3", 30, 4),
	new AntiFraud.Item("4", "Item4", 40, 5)
};

gate.MakeRequest(items, nInstallments, card, operatorIndex);
```


## Authors
* **Carlos Monteiro** - [carlosfem](https://github.com/carlosfem)

## License

This project is licensed under the MIT License - see the LICENSE.md file for details.