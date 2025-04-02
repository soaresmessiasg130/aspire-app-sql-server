var builder = DistributedApplication.CreateBuilder(args);

var sqlServerPassword = builder.AddParameter("sqlServer-password");

var sqlServer = builder.AddSqlServer("sqlServer", sqlServerPassword, port: 1234);

var customerCRMDB = sqlServer.AddDatabase("database");

var apiService = builder.AddProject<Projects.AspireAppSqlServer_ApiService>("apiservice")
    .WithReference(customerCRMDB)
    .WaitFor(customerCRMDB);

builder.AddProject<Projects.AspireAppSqlServer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
