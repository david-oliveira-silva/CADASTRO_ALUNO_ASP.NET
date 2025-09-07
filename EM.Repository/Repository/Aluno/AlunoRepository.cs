using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Data;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            FirebirdConnection.OpenConnection(fbConnection);

            string queryInsert = "INSERT INTO alunos (matricula,nomeAluno,CPF,dtNascimento,sexo,cidadeID_) VALUES (@matricula,@nomeAluno,@CPF,@dtNascimento,@Sexo,cidadeID_)";

            using (var cmdInsert = new FbCommand(queryInsert, fbConnection))
            {

                cmdInsert.Parameters.AddWithValue(@"matricula", alunoModel.matricula);
                cmdInsert.Parameters.AddWithValue(@"nomeAluno", alunoModel.nome);
                cmdInsert.Parameters.AddWithValue(@"CPF", alunoModel.CPF);
                cmdInsert.Parameters.AddWithValue(@"dtNascimento", alunoModel.dtNascimento);
                cmdInsert.Parameters.AddWithValue(@"sexo", alunoModel.sexo);
                cmdInsert.Parameters.AddWithValue(@"cidadeID_", alunoModel.cidadeID_);

                cmdInsert.ExecuteNonQuery();
            }
            FirebirdConnection.CloseConnection(fbConnection);

        }

        public void Deletar(AlunoModel alunoModel)
        {
            FirebirdConnection.OpenConnection(fbConnection);

            string queryDelete = "DELETE FROM alunos WHERE matricula = @matricula";
            using (var cmdDelete = new FbCommand(queryDelete, fbConnection))
            {
                cmdDelete.Parameters.AddWithValue("@matricula", alunoModel.matricula);
                cmdDelete.ExecuteNonQuery();
            }
            FirebirdConnection.CloseConnection(fbConnection);
        }

        public void Editar(AlunoModel alunoModel)
        {
            FirebirdConnection.OpenConnection(fbConnection);

            string queryUpdate = "UPDATE alunos SET nomeAluno = @nomeAluno,CPF = @CPF,dtNascimento = @dtNascimento,sexo = @sexo,cidadeID_ = @cidadeID_ WHERE matricula = @matricula";

            using (var cmdUpdate = new FbCommand(queryUpdate, fbConnection))
            {
                cmdUpdate.Parameters.AddWithValue(@"nomeAluno", alunoModel.nome);
                cmdUpdate.Parameters.AddWithValue(@"CPF", alunoModel.CPF);
                cmdUpdate.Parameters.AddWithValue(@"dtNascimento", alunoModel.dtNascimento);
                cmdUpdate.Parameters.AddWithValue(@"sexo", alunoModel.sexo);
                cmdUpdate.Parameters.AddWithValue(@"cidadeID_", alunoModel.cidadeID_);
                cmdUpdate.ExecuteNonQuery();
            }
            FirebirdConnection.CloseConnection(fbConnection);

        }

        public List<AlunoModel> Listar()
        {

            FirebirdConnection.OpenConnection(fbConnection);

            List<AlunoModel> listAlunos = new List<AlunoModel>();

            string querySelect = "SELECT * FROM alunos";

            using (var cmdSelect = new FbCommand(querySelect, fbConnection))
            {
                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var aluno = new AlunoModel()
                        {
                            nome = reader.GetString(reader.GetOrdinal("alunoNome")),
                            CPF = reader.GetString(reader.GetOrdinal("CPF")),
                            dtNascimento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("dtNascimento"))),
                            sexo = (SexoEnum)reader.GetInt32(reader.GetOrdinal("sexo")),
                            cidadeID_ = reader.GetInt32(reader.GetOrdinal("cidadeID_"))
                        };
                        listAlunos.Add(aluno);
                    }
                }
                FirebirdConnection.CloseConnection(fbConnection);
                return listAlunos;
            }
        }
        public List<AlunoModel> buscarPorMatricula(long matricula)
        {
            FirebirdConnection.OpenConnection(fbConnection);
            List<AlunoModel> listAlunos = new List<AlunoModel>();

            string querySelect = "SELECT * FROM alunos WHERE matricula = @matricula";

            using (var cmdSelect = new FbCommand(querySelect, fbConnection))
            {
                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var aluno = new AlunoModel()
                        {
                            nome = reader.GetString(reader.GetOrdinal("alunoNome")),
                            CPF = reader.GetString(reader.GetOrdinal("CPF")),
                            dtNascimento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("dtNascimento"))),
                            sexo = (SexoEnum)reader.GetInt32(reader.GetOrdinal("sexo")),
                            cidadeID_ = reader.GetInt32(reader.GetOrdinal("cidadeID_"))
                        };
                        listAlunos.Add(aluno);
                    }
                }
                FirebirdConnection.CloseConnection(fbConnection);
                return listAlunos;
            }

          


        }
    }
}
