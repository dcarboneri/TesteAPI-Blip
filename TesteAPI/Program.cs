using TesteAPI.Interfaces;
using TesteAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMainService, MainService>(provider =>
    new MainService("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"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
