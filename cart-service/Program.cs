using System.Text.Json.Serialization;
using cart_service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(o => o.AddPolicy("AllAccessPolicy", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

builder.Services.AddSingleton<ShoppingListService, ShoppingListService>();
builder.Services.AddSingleton<ShoppingItemService, ShoppingItemService>();

// builder.WebHost.UseUrls("http://localhost:3100");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllAccessPolicy");
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.MapControllers();

app.Run();