En este repositorio trabajaremos con un BackEnd para una aplicación de productos, pero con diferente estructura para una mejor escalabilidad y mantenimiento. Para ello se ha utilizado una base de datos SQLServer y .NET para la lógica e interactuar con la base de datos.
Para lograrlo, se ha dividido el backend en 3 capas: Infrastructure, Presentation y Core:
- **Infrastructure**: Esta capa contiene el [Contexto de la BD](Productos.BackEnd.Infrastructure/Context/ProductDBContext.cs), las [Migraciones](Productos.BackEnd.Infrastructure/Migrations/), [registro del repositorio](Productos.BackEnd.Infrastructure/Registration/InfrastructureRegistration.cs) y el [repositorio](Productos.BackEnd.Infrastructure/Repositories/ProductRepository.cs)
- **Presentation**: Se encarga principalmente de la parte del [Controller](Productos.BackEnd.WebAPI/Controllers/), junto a la [construcción de WatchDog y Swagger](Productos.BackEnd.WebAPI/Builders/) y su [correspondiente registro](Productos.BackEnd.WebAPI/Registration).
- *Adicionalmente, aquí es donde encontraremos el [Program.cs](Productos.BackEnd.WebAPI/Program.cs)*
- **Core**: Realiza las funciones de la lógica de negocio. Se divide a su vez en 3 partes:
- **Domain**: Dispone de los [Modelos](Productos.BackEnd.Domain/Models), [Entidades](Productos.BackEnd.Domain/Entities), y una serie de interfaces que se implementarán en servicios y repositorios: [Interfaces](Productos.BackEnd.Domain/Contracts)
- **Business**: Principalmente contendrá al [Servicio](Productos.BackEnd.Business/Services/ProductService.cs) que trabajará con el repositorio definido en la capa Infrastructure.
- **Application**: Contiene el [Handler](Productos.BackEnd.Application/Features/Products/ProductHandler.cs), junto a todas las instrucciones de [Consultas](Productos.BackEnd.Application/Features/Products/Queries/), [Otras operaciones CRUD](Productos.BackEnd.Application/Features/Products/Commands/) y [Validaciones](Productos.BackEnd.Application/Features/Products/Validators/ProductModelValidatos.cs)
- Además posee el [Registro y Configuración](Productos.BackEnd.Application/Registration/) y el [MappingProfile](Productos.BackEnd.Application/Mappings/ProductProfile.cs)
- *Todas estas capas exceptuando la de Domain deben tener pruebas unitarias en sus elementos clave. Para ello, se han creado 4 proyectos de testeo:*
- [Handler de Application](Productos.BackEnd.Application.Tests/ProductHandlerTest.cs)
- [Service en Business](Productos.BackEnd.Business.Tests/ProductServiceTest.cs)
- [Repository en Infrastructure](Productos.BackEnd.Infrastructure.Tests/ProductRepositoryTest.cs)
- [Controller en WebAPI](Productos.BackEnd.WebAPI.Tests/ProductControllerTest.cs)
