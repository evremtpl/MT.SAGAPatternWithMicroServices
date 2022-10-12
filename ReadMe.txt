Hello,

Project on distribute transaction with Saga Orchestration Pattern in MicroServices,

.Net Core 5.0 has also been developed.
There are two microservices, PersonService and ReportService.

In terms of diversity as a database, MSSQL was used in Person microservice, 
Postgres was used in Report Microservice. While communicating with the database,
EntityFramework Core, one of the ORM tools was used. And also Repository Pattern was used.

The project was developed as NLayerArchitecture and coding was done in accordance with SOLID.

The project was developed step by step in the development branch, committed and merged into the master branch.
The services communicate over HTTP.

The SAGA pattern implementation in the project was carried out using the Masstransit Framework.
RabbitMq, one of the Message Broker systems used for async communication of microservices,
has been implemented over the Masstransit Framework.

The scenario here is that the Person service keeps persons and contact information in its own database.
The user requests to create a report with his/her own user id using the Report service. 
The request is sent to the Message Broker. Here, SagaStateMachine listens to the Queue and
publishes the ValidateEvent for the request to be validated and sends it to the queue. 
With the verified request, the report is created and the report state is marked as complete in the report database.
If there is a situation that needs to be canceled as a result of the verification, 
CancelledEvent is Published and the relevant consumer cancels the report and updates its status and CancelledDate
Person service must be called in order to carry out operations related to the person.
Report service should be called for report request and report fetching operations.
In report requests, not all reports are sent, but the id is sent, thereby reducing the network load.
The generated reports are saved in the report service wwwroot/files directory.

Services start from ports 5002 and 5003 in Kestrel. It is stated in the ports.txt document.
The migration structure of the project was developed as code first,
databases will be created when the person and report services are up.
Conn strings are located in appsettings.json file of services.
There are swagger integrations of services in the project.
