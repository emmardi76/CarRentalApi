# CarRentalApi

1.-Install Docker Desktop from microsoft store (if you don't have it already).

2.-Open the power shell and go to the root folder of the solution.

3.-Launch docker desktop.

4.-Execute ```docker compose up -d```

5.-Debug the application with visual studio 2022.

6.-Respond yes to the questions about trusting the self signed certificate and install it.# CarRentalApi

# Software Architecture Decisions.

1.- Decided to use DDD (Domain Driven Design) as the main architecture pattern for the solution.

	1.1.-Layers and responsabilities chosen:
		- Domain Layer: Contains the business logic, domain entities and repository interfaces. Located in folder Domain.
		- Application Layer: Implement adaptations between entities and dtos, orchestrate domain service calls and ensures transactional behaviour. Located in folder Application.
		- Persistence Layer (Infrastructure Persistence): Contains ORM configuration (entity framework core) and repository implementations. Located in folder persistence. 
		- CrossCutting Layer (Infrastructure Transversal): Contains exceptions definitions. Located in folder CrossCutting.
		- Communications Layer (Api): Contains controllers and call the Application Layer Services. Main Api project.
		- Every layer should be separated in different projects in a real world project, here has been simplified for the sake of the exercise.

	1.2.-The patterns used have been
		- Repository Pattern. Uses dbcontext.
		- Unit of Work Pattern. Implemented using EF Core DbContext (Savechanges).
		- Dependency Injection. Configured in Program.cs file.
		- Adapter Pattern.

# Specific technical decisions.

1.- Decided to use .Net 8 as main framework for the solution. Because I know it well and didn't test .net 10 yet.

2.- We use docker to containerize the application database (postgresql 15.3) as is shown in the docker-compose.yml file. 

3.- Decided to use Entity Framework Core as ORM for the solution. Robust ORM widely used in the industry.

4.- Started using Mapster as adapter between entities and dtos. Automapper is the alternative I know better but know is under license. I had to adapt myself to use this new one.



