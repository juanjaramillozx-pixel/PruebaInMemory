using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Repositories;
using Project.Domain.Repositories;
using Project.Application.UseCases;
using Project.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProjectDb"));

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// UseCases
builder.Services.AddScoped<CreateCustomerUseCase>();
builder.Services.AddScoped<GetAllCustomersUseCase>();
builder.Services.AddScoped<UpdateCustomerUseCase>();
builder.Services.AddScoped<DeleteCustomerUseCase>();

// Services
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Controllers
app.MapControllers();

app.Run();
