# ProductApi
This a Web API build to demonstrate the .net core API features. This Solution consists of four layers.

**ProductApi.Web** is the web api layer where the controller classes are defined.

**ProductApi.BusinessLayer** is the service layer project where all the business logic and validations are performed. 

**ProductApi.Core** is the core layer where all the models, Dto's and helper classes are defined.

**ProductApi.Data** is the database layer which has all the DB Contexts and migrations required by the EF Core to build the schema. It also has the implementation of all the repositories defined in the **ProductApi.Core.Repositories**.

**ProductApi.UnitTests** is the unit test projects. I have implemented the different unit test which covers all the three public methods in the ProductService class. 



