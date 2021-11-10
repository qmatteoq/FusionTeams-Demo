using CustomersServices.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swagger, httpReq) =>
    {
        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
    });
    c.SerializeAsV2 = true;
});
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.UseHttpsRedirection();

app.MapGet("/customers", (HttpContext context, ICustomerService customerService) =>  {
    var customers = customerService.GetCustomers();
    return customers;
});

app.Run();
