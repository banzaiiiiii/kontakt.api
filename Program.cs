using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Kontakt.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        builder => builder.WithOrigins("google.com"))
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<IContactRepo, ContactRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    PrepDb.PrepPopulation(app);
}

app.UseAuthorization();
app.UseCors();
app.MapControllers();


app.Run();
