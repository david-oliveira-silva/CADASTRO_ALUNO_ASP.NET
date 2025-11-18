using FirebirdSql.Data.FirebirdClient;

namespace EM.Repository.Data
{
    public class FirebirdConnection
    {
        private static string? conexaoString;
        public static FbConnection GetFbConnection()
        {
            return new FbConnection(conexaoString);
        }
        public static void inicializar(string connectionString)
        {
            conexaoString = connectionString;
        }
        public static void OpenConnection(FbConnection fbConnection)
        {
            try
            {
                fbConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void CloseConnection(FbConnection fbConnection)
        {
            try
            {
                fbConnection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
