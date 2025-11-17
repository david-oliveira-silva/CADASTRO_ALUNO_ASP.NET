using EM.Repository.Data;
using EM.Repository.Repository.Aluno;
using EM.Repository.Repository.Cidade;
using EM.Service.Service;

namespace EM.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            string conexaoString = builder.Configuration.GetConnectionString("FirebirdConnection");
            FirebirdConnection.inicializar(conexaoString);

            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<AlunoService>();
            builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
            builder.Services.AddScoped<CidadeService>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Aluno}/{action=ListarAlunos}/{id?}");

            app.Run();
        }
    }
}
