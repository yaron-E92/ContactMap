using ContactMap.Infrastructure;
using ContactMap.Application;
using ContactMap.Application.UseCases;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEventAggregator();
// Register DbContext with in-memory database
builder.Services.AddInfrastructureDI();
// Register use case
builder.Services.AddScoped<ISearchPeopleUseCase, SearchPeopleUseCase>();
// Register controllers
builder.Services.AddControllers();
WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
