using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoProdutos : IServicoProduto
    {
        private readonly IProduto _Produto;
        public ServicoProdutos(IProduto IProduto)
        {
            _Produto = IProduto;
        }

        public async Task AdicionarProduto(Produtos produto)
        {
            await _Produto.Adicionar(produto);
        }

        public async Task AtualizaProduto(Produtos produto)
        {
            await _Produto.Atualizar(produto);
        }

        public async Task ExcluirProduto(Produtos produto)
        {
            await _Produto.Excluir(produto);
        }

        public async Task<List<Produtos>> ListarProdutosAtivos(Produtos filtro, int pagina, int numeroRegistros)
        {
            List<Produtos> produtosFiltro = new List<Produtos>();
            DateTime dataVazia = new DateTime(1, 1, 1, 0, 0, 0);

            produtosFiltro = await _Produto.ListarProdutos(x =>
              ((x.CodProduto == filtro.CodProduto && filtro.CodProduto > 0) || filtro.CodProduto == 0) ||
              (x.DsProduto == filtro.DsProduto) &&
              (x.Ativo == true) ||
              ((x.DtFabricacao.Day == filtro.DtFabricacao.Day && x.DtFabricacao.Month == filtro.DtFabricacao.Month
              && x.DtFabricacao.Year == filtro.DtFabricacao.Year && filtro.DtFabricacao != dataVazia) || filtro.DtFabricacao == dataVazia) ||
              ((x.DtValidade.Day == filtro.DtValidade.Day && x.DtValidade.Month == filtro.DtValidade.Month && x.DtValidade.Year == filtro.DtValidade.Year && filtro.DtValidade != dataVazia) || filtro.DtValidade == dataVazia) ||
              ((x.CodFornecedor == filtro.CodFornecedor && filtro.CodFornecedor != "")) ||
              (x.DsFornecedor.Contains(filtro.DsFornecedor)) ||
              ((x.CNPJ_Fornecedor == filtro.CNPJ_Fornecedor && string.IsNullOrWhiteSpace(filtro.CNPJ_Fornecedor) == false) || string.IsNullOrWhiteSpace(filtro.CNPJ_Fornecedor))
            );

            return produtosFiltro.Skip(((pagina - 1) * numeroRegistros))
                .Take(numeroRegistros)
                .ToList();
        }
    }
}
