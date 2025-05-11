using Order.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.ConfigureSolution();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom Middleware
app.UseMiddleware<Order.API.Middlewares.ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
