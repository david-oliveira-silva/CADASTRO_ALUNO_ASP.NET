using EM.Repository.Repository.Cidade;
using EM.Service.Service;

namespace EM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ICidadeRepository, CidadeRepository>(); // <-- se existir interface
            builder.Services.AddScoped<CidadeService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            

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
}
