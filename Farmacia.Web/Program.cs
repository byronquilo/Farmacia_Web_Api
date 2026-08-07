using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Farmacia.web.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PharmacyContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TallerConnection")));
builder.Services.AddControllers()

.AddJsonOptions(Options=>
 {
     Options.JsonSerializerOptions.ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
 }
);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();