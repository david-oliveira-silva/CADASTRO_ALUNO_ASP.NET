using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Data;
using FirebirdSql.Data.FirebirdClient;

namespace EM.Repository.Repository.Cidade
{
    public class CidadeRepository : ICidadeRepository
    {
        FbConnection fbConnection;

        public CidadeRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }

        public void Cadastrar(CidadeModel cidadeModel)
        {

            try
            {
                FirebirdConnection.OpenConnection(fbConnection);
                string queryInsert = "INSERT INTO cidades(cidadeNome,cidadeUF) VALUES (@cidadeNome,@cidadeUF)";

                using (var cmdInsert = new FbCommand(queryInsert, fbConnection))
                {

                    cmdInsert.Parameters.AddWithValue("@cidadeNome", cidadeModel.cidadeNome);
                    cmdInsert.Parameters.AddWithValue("@cidadeUF", cidadeModel.cidadeUF);
                    cmdInsert.ExecuteNonQuery();

                }
            }
            finally
            {

                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Deletar(CidadeModel cidadeModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryDelete = "DELETE FROM cidades WHERE cidadeID = @cidadeID";

                using (var cmdDelete = new FbCommand(queryDelete, fbConnection))
                {

                    cmdDelete.Parameters.AddWithValue("@cidadeID", cidadeModel.cidadeID);
                    cmdDelete.ExecuteNonQuery();

                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public void Editar(CidadeModel cidadeModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                var queryUpdate = "UPDATE cidades SET cidadeNome = @cidadeNome,cidadeUF = @cidadeUF WHERE cidadeID = @cidadeID";

                using (var cmdUpdate = new FbCommand(queryUpdate, fbConnection))
                {
                    cmdUpdate.Parameters.AddWithValue("@cidadeId", cidadeModel.cidadeID);
                    cmdUpdate.Parameters.AddWithValue("@cidadeNome", cidadeModel.cidadeNome);
                    cmdUpdate.Parameters.AddWithValue("@cidadeUF", cidadeModel.cidadeUF);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }

        }

        public List<CidadeModel> Listar()
        {
            List<CidadeModel> listCidades = new List<CidadeModel>();
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string querySelect = "SELECT * FROM cidades";

                using (var cmdSelect = new FbCommand(querySelect, fbConnection))
                {

                    using (var reader = cmdSelect.ExecuteReader())
                    {
                        int cidadeIdOrdinal = reader.GetOrdinal("cidadeID");
                        int cidadeNomeOrdinal = reader.GetOrdinal("cidadeNome");
                        int cidadeUfOrdinal = reader.GetOrdinal("cidadeUF");

                        while (reader.Read())
                        {
                            var cidade = new CidadeModel()
                            {
                                cidadeID = reader.GetInt32(cidadeIdOrdinal),
                                cidadeNome = reader.GetString(cidadeNomeOrdinal),
                                cidadeUF = Enum.Parse<UF>(reader.GetString(cidadeUfOrdinal))

                            };
                            listCidades.Add(cidade);
                        }
                    }
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
            return listCidades;
        }
    }
}
