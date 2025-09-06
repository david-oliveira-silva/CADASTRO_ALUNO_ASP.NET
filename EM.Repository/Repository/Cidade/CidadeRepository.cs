using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Data;
using FirebirdSql.Data.FirebirdClient;

namespace EM.Repository.Repository.Cidade
{
    public class CidadeRepository:ICidadeRepository
    {
        FbConnection fbConnection;

        public CidadeRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }

        public void Cadastrar(CidadeModel cidadeModel)
        {
            FirebirdConnection.OpenConnection(fbConnection);
            string queryInsert = "INSERT INTO cidades(cidadeNome,cidadeUF) VALUES (@cidadeNome,@cidadeUF)";

            using (var cmdInsert = new FbCommand(queryInsert, fbConnection))
            {

                cmdInsert.Parameters.AddWithValue("@cidadeNome", cidadeModel.cidadeNome);
                cmdInsert.Parameters.AddWithValue("@cidadeUF", cidadeModel.cidadeUF);
                cmdInsert.ExecuteNonQuery();

            }

            FirebirdConnection.CloseConnection(fbConnection);
        }

        public void Deletar(CidadeModel cidadeModel)
        {
            FirebirdConnection.OpenConnection(fbConnection);

            string queryDelete = "DELETE FROM cidades WHERE cidadeID = @cidadeID";

            using (var cmdDelete = new FbCommand(queryDelete, fbConnection))
            {

                cmdDelete.Parameters.AddWithValue("@cidadeID", cidadeModel.cidadeID);
                cmdDelete.ExecuteNonQuery();

            }
            FirebirdConnection.CloseConnection(fbConnection);
        }

        public void Editar(CidadeModel cidadeModel)
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
            FirebirdConnection.CloseConnection(fbConnection);

        }

        public List<CidadeModel> Listar()
        {
            List<CidadeModel> listCidades = new List<CidadeModel>();
            FirebirdConnection.OpenConnection(fbConnection);

            string querySelect = "SELECT * FROM cidades";

            using (var cmdSelect = new FbCommand(querySelect, fbConnection))
            {

                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cidade = new CidadeModel()
                        {
                            cidadeID = reader.GetInt32(reader.GetOrdinal("cidadeID")),
                            cidadeNome = reader.GetString(reader.GetOrdinal("cidadeNome")),
                            cidadeUF = Enum.Parse<UF>(reader.GetString(reader.GetOrdinal("cidadeUF")))


                        };
                        listCidades.Add(cidade);
                    }
                }
            }
            FirebirdConnection.CloseConnection(fbConnection);
            return listCidades;
        }
    }
}
