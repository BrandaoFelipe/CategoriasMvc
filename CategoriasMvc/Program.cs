using CategoriasMvc.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient("CategoriasApi", c =>
        {
            c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CategoriasApi"]); //endereço base alocado no appsettings.Json
        });
        builder.Services.AddHttpClient("ProdutosApi", c =>
        {
            c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProdutosApi"]); //endereço base alocado no appsettings.Json
        });
        builder.Services.AddHttpClient("AutenticaApi", c =>
        {
            c.BaseAddress = new Uri(builder.Configuration["ServiceUri:AutenticaApi"]); //cliente nomeado
            c.DefaultRequestHeaders.Accept.Clear(); //limpa cabeçalho
            c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        });
        builder.Services.AddScoped<ICategoriaService, CategoriaService>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();
        builder.Services.AddScoped<IAutenticacao, Autenticacao>();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}