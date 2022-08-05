using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

//Adiciona a configuracao no container nativo para Injecao de Dependencia
//atraves do metodo de extensao de CleanArchMVC.Infra.IoC.DependencyInjection...
builder.Services.AddInfrastructure(builder.Configuration);

//Ativa Autentica��o e valida��o de Token
builder.Services.AddInfrastructureJWT(builder.Configuration);

//Adiciona Servi�o para Customiza��o Swagger (Autentica��o Bearer)
builder.Services.AddInfrastructureSwagger();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

//Ativa a Autentica��o
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
