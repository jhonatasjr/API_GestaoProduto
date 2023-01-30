using Dominio.Interfaces.Genericos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IProduto : IGenericos<Produtos>
    {
        Task<List<Produtos>> ListarProdutos(Expression<Func<Produtos, bool>> exProduto);
    }
}
