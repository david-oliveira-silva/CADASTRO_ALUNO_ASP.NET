using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Data;
using FirebirdSql.Data.FirebirdClient;


namespace EM.Repository.Repository.Aluno
{
    public class AlunoRepository : IAlunoRepository
    {
        FbConnection fbConnection;

        public AlunoRepository()
        {
            fbConnection = FirebirdConnection.GetFbConnection();
        }
        public void Cadastrar(AlunoModel alunoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);



                string queryInsert = "INSERT INTO alunos (matricula,alunoNome,CPF,dtNascimento,sexo,cidadeID_) VALUES (@matricula,@alunoNome,@CPF,@dtNascimento,@sexo,@cidadeID_)";

                using (var cmdInsert = new FbCommand(queryInsert, fbConnection))
                {

                    cmdInsert.Parameters.AddWithValue(@"matricula", alunoModel.matricula);
                    cmdInsert.Parameters.AddWithValue(@"alunoNome", alunoModel.nome);
                    cmdInsert.Parameters.AddWithValue(@"CPF", alunoModel.CPF);
                    cmdInsert.Parameters.AddWithValue(@"dtNascimento", alunoModel.dtNascimento);
                    cmdInsert.Parameters.AddWithValue(@"sexo", alunoModel.sexo);
                    cmdInsert.Parameters.AddWithValue(@"cidadeID_", alunoModel.cidadeID_);

                    cmdInsert.ExecuteNonQuery();
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);

            }
        }

        public void Deletar(AlunoModel alunoModel)
        {

            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryDelete = "DELETE FROM alunos WHERE matricula = @matricula";
                using (var cmdDelete = new FbCommand(queryDelete, fbConnection))
                {
                    cmdDelete.Parameters.AddWithValue("@matricula", alunoModel.matricula);
                    cmdDelete.ExecuteNonQuery();
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }








        public void Editar(AlunoModel alunoModel)
        {
            try
            {
                FirebirdConnection.OpenConnection(fbConnection);

                string queryUpdate = "UPDATE alunos SET alunoNome = @alunoNome,CPF = @CPF,dtNascimento = @dtNascimento,sexo = @sexo,cidadeID_ = @cidadeID_ WHERE matricula = @matricula";

                using (var cmdUpdate = new FbCommand(queryUpdate, fbConnection))
                {
                    cmdUpdate.Parameters.AddWithValue(@"matricula", alunoModel.matricula);

                    cmdUpdate.Parameters.AddWithValue(@"alunoNome", alunoModel.nome);
                    cmdUpdate.Parameters.AddWithValue(@"CPF", alunoModel.CPF);
                    cmdUpdate.Parameters.AddWithValue(@"dtNascimento", alunoModel.dtNascimento);
                    cmdUpdate.Parameters.AddWithValue(@"sexo", alunoModel.sexo);
                    cmdUpdate.Parameters.AddWithValue(@"cidadeID_", alunoModel.cidadeID_);
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }
        }

        public List<AlunoModel> Listar()
        {


            List<AlunoModel> listAlunos = new List<AlunoModel>();


            try
            {
                FirebirdConnection.OpenConnection(fbConnection);



                string querySelect = "SELECT * FROM alunos a INNER JOIN cidades c on a.cidadeID_ = c.cidadeID";

                using (var cmdSelect = new FbCommand(querySelect, fbConnection))
                {
                    using (var reader = cmdSelect.ExecuteReader())
                    {
                        int matriculaOrdinal = reader.GetOrdinal("matricula");
                        int nomeOrdinal = reader.GetOrdinal("alunoNome");
                        int cpfOrdinal = reader.GetOrdinal("CPF");
                        int dtNascimentoOrdinal = reader.GetOrdinal("dtNascimento");
                        int sexoOrdinal = reader.GetOrdinal("sexo");
                        int cidadeID_Ordinal = reader.GetOrdinal("cidadeID_");
                        int cidadeNomeOrdinal = reader.GetOrdinal("cidadeNome");
                        int cidadeUFOrdinal = reader.GetOrdinal("cidadeUF");

                        while (reader.Read())
                        {

                            var aluno = new AlunoModel()
                            {
                                matricula = reader.GetInt64(matriculaOrdinal),
                                nome = reader.GetString(nomeOrdinal),
                                CPF = reader.IsDBNull(cpfOrdinal) ? null : reader.GetString(cpfOrdinal),
                                dtNascimento = reader.IsDBNull(dtNascimentoOrdinal) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(dtNascimentoOrdinal)),
                                sexo = (SexoEnum)reader.GetInt32(sexoOrdinal),
                                cidade = new CidadeModel()
                                {
                                    cidadeID = reader.GetInt32(cidadeID_Ordinal),
                                    cidadeNome = reader.GetString(cidadeNomeOrdinal),
                                    cidadeUF = (UF)Enum.Parse(typeof(UF), (reader.GetString(cidadeUFOrdinal)))
                                }


                            };
                            listAlunos.Add(aluno);
                        }
                    }
                }
            }
            finally
            {
                FirebirdConnection.CloseConnection(fbConnection);
            }

            return listAlunos;
        }
    }


}



