using EM.Domain.Generic;
using EM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Repository.Repository.Aluno
{
    public interface IAlunoRepository:IGeneric<AlunoModel>
    {

        List<AlunoModel> buscarPorMatricula(long matricula);
    }
}
