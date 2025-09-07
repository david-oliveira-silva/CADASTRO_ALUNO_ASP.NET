using EM.Domain.Enum;
using EM.Domain.Models;
using EM.Repository.Data;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            FirebirdConnection.CloseConnection(fbConnection);

        }

        public List<AlunoModel> Listar()
        {

            FirebirdConnection.OpenConnection(fbConnection);

            List<AlunoModel> listAlunos = new List<AlunoModel>();

            string querySelect = "SELECT * FROM alunos a INNER JOIN cidades c on a.cidadeID_ = c.cidadeID";

            using (var cmdSelect = new FbCommand(querySelect, fbConnection))
            {
                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var aluno = new AlunoModel()
                        {
                            matricula = reader.GetInt64(reader.GetOrdinal("matricula")),
                            nome = reader.GetString(reader.GetOrdinal("alunoNome")),
                            CPF = reader.GetString(reader.GetOrdinal("CPF")),
                            dtNascimento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("dtNascimento"))),
                            sexo = (SexoEnum)reader.GetInt32(reader.GetOrdinal("sexo")),
                            cidade = new CidadeModel()
                            {
                                cidadeID = reader.GetInt32(reader.GetOrdinal("cidadeID_")),
                                cidadeNome = reader.GetString(reader.GetOrdinal("cidadeNome")),
                                cidadeUF = (UF)Enum.Parse(typeof(UF),(reader.GetString(reader.GetOrdinal("cidadeUF"))))
                            }


                        };
                        listAlunos.Add(aluno);
                    }
                }
                FirebirdConnection.CloseConnection(fbConnection);
                return listAlunos;
            }
        }
        public AlunoModel buscarPorMatricula(long matricula)
        {
            FirebirdConnection.OpenConnection(fbConnection);
            AlunoModel aluno = null;

            string querySelect = "SELECT * FROM alunos WHERE matricula = @matricula";

            using (var cmdSelect = new FbCommand(querySelect, fbConnection))
            {
                cmdSelect.Parameters.AddWithValue("@matricula", matricula);
                    
                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var alunos = new AlunoModel()
                        {
                            nome = reader.GetString(reader.GetOrdinal("alunoNome")),
                            CPF = reader.GetString(reader.GetOrdinal("CPF")),
                            dtNascimento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("dtNascimento"))),
                            sexo = (SexoEnum)reader.GetInt32(reader.GetOrdinal("sexo")),
                            cidadeID_ = reader.GetInt32(reader.GetOrdinal("cidadeID_"))
                        };
                       
                    }
                }
                FirebirdConnection.CloseConnection(fbConnection);
                return aluno;


            }




        }
    }
}
