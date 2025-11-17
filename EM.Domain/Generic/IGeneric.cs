
namespace EM.Domain.Generic;

using System.Collections.Generic;
public interface IGeneric<T>
{
    void Cadastrar(T entity);

    void Deletar(T entity);

    void Editar(T entity);

    List<T> Listar();
}

