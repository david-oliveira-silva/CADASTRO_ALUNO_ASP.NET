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
        public void Cadastrar(CidadeModel cidadeModel)
        {
            FirebirdConnection.OpenConnection(fbConnection);

            string queryInsert = "INSERT INTO alunos (matricula,nomeAluno,sexo,cidadeID_)";
            FirebirdConnection.CloseConnection(fbConnection);

        }

        public void Deletar(CidadeModel cidadeModel)
        {
            throw new NotImplementedException();
        }

        public void Editar(CidadeModel cidadeModel)
        {
            throw new NotImplementedException();
        }

        public List<CidadeModel> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
