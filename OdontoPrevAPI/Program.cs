using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Repositories;
using OdontoPrevAPI.Services;
using OdontoPrevAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

//Conex�o com banco Oracle
var connectionString = ConfigManager.Instance.ConnectionString;
builder.Services.AddDbContext<OdontoPrevContext>(options =>
    options.UseOracle(connectionString), ServiceLifetime.Scoped);

//Registro dos reposit�rios
builder.Services.AddScoped<PacienteRepository>();
builder.Services.AddScoped<TratamentoRepository>();
builder.Services.AddScoped<SinistroRepository>();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//Servi�os e controllers
builder.Services.AddHttpClient<ViaCepService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Configura��o do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OdontoPrev API",
        Version = "v1",
        Description = "API para gerenciamento de pacientes, tratamentos, sinistros e an�lise preditiva via IA."
    });

    //Inclui coment�rios XML
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

//Pipeline HTTP
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
