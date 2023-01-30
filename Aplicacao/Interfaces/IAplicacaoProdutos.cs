using Aplicacao.Interfaces.Genericos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IAplicacaoProdutos : IGenericaAplicacao<Produtos>
    {
        Task AdicionarProduto(Produtos produto);
        Task AtualizaProduto(Produtos produto);
        Task ExcluirProduto(Produtos produto);
        Task<List<Produtos>> ListarProdutosAtivos(Produtos filtro, int pagina, int numeroRegistros);
    }
}
