using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

//Adiciona a configuracao no container nativo para Injecao de Dependencia
//atraves do metodo de extensao de CleanArchMVC.Infra.IoC.DependencyInjection...
builder.Services.AddInfrastructure(builder.Configuration); //Adaptado do .Net 5

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

SeedUserRoles(app);

//Adiciona Servico de Autenticacao
app.UseStatusCodePages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

////Seed De Usuarios Iniciais
void SeedUserRoles(IApplicationBuilder app)
{
    //Gera escopo para acesso ao Servico ISeedUserRoleInitial
    using (var serviceScope = app.ApplicationServices.CreateScope()) 
    {
        var seed = serviceScope.ServiceProvider
                               .GetService<ISeedUserRoleInitial>();
        seed.SeedRoles();
        seed.SeedUsers();
    }
}
