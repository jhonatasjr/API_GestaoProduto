using Dominio.Interfaces.Genericos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Interfaces.InterfaceServico
{
    public interface IServicoProduto
    {
        Task AdicionarProduto(Produtos produto);
        Task AtualizaProduto(Produtos produto);
        Task ExcluirProduto(Produtos produto);
        Task<List<Produtos>> ListarProdutosAtivos(Produtos filtro, int pagina, int numeroRegistros);
    }
}
