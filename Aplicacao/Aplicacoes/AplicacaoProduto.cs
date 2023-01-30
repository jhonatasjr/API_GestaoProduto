using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServico;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoProduto : IAplicacaoProdutos
    {
        IProduto _IProduto;
        IServicoProduto _IServicoProduto;
        public AplicacaoProduto(IProduto IProduto, IServicoProduto IServicoProduto)
        {
            _IProduto = IProduto;
            _IServicoProduto = IServicoProduto;
        }

        public async Task AdicionarProduto(Produtos produto)
        {
            await _IServicoProduto.AdicionarProduto(produto);
        }

        public async Task AtualizaProduto(Produtos produto)
        {
            await _IServicoProduto.AtualizaProduto(produto);
        }

        public async Task ExcluirProduto(Produtos produto)
        {
            await _IServicoProduto.ExcluirProduto(produto);
        }

        public async Task<List<Produtos>> ListarProdutosAtivos(Produtos filtro, int pagina, int numeroRegistros)
        {
            return await _IServicoProduto.ListarProdutosAtivos(filtro, pagina, numeroRegistros);
        }




        //Com assinatura assincrona
        public async Task Adicionar(Produtos Objeto)
        {
            await _IProduto.Adicionar(Objeto);
        }

        public async Task Atualizar(Produtos Objeto)
        {
            await _IProduto.Atualizar(Objeto);
        }

        public async Task<Produtos> BuscaPorCod(int cod)
        {
            return await _IProduto.BuscaPorCod(cod);
        }

        public async Task Excluir(Produtos Objeto)
        {
            await _IProduto.Excluir(Objeto);
        }

        public async Task<List<Produtos>> Listar()
        {
            return await _IProduto.Listar();
        }

    }
}
