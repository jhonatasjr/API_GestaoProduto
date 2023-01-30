using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IAplicacaoProdutos _IAplicacaoProduto;
        public ProdutosController(IAplicacaoProdutos IAplicacaoProduto)
        {
            _IAplicacaoProduto = IAplicacaoProduto;
        }

        [Produces("application/json")]
        [HttpGet("/api/BuscaPorCodigo")]
        public async Task<ActionResult> BuscaPorCodigo(int CodProduto)
        {
            var produto = await _IAplicacaoProduto.BuscaPorCod(CodProduto);

            if (produto == null)
            {
                return BadRequest("Erro! Produto não encontrado!");
            }
            return Ok(produto);
        }

        [Produces("application/json")]
        [HttpPost("/api/ListarProdutos")]
        public async Task<ActionResult> ListarProdutos(Produtos filtro, int pagina, int numeroRegistros)
        {
            var produtos = await _IAplicacaoProduto.ListarProdutosAtivos(filtro, pagina, numeroRegistros);

            if (produtos.Count == 0)
            {
                return BadRequest("Erro! Nenhum produto encontrado!");
            }
            return Ok(produtos);
        }

        [Produces("application/json")]
        [HttpPost("/api/AdicionarProduto")]
        public async Task<ActionResult> AdicionarProduto([FromBody] ProdutoModelView produtoView)
        {
            #region Validações
            if (string.IsNullOrWhiteSpace(produtoView.DescricaoProduto))
            {
                return BadRequest("Erro! Descrição do produto não preenchida!");
            }
            if (produtoView.DataFabricacao >= produtoView.DataValidade)
            {
                return BadRequest("Erro! A data de fabricação não pode ser menor ou igual a data de validade do produto!");
            }
            if (!Validar_CNPJ(produtoView.CNPJ_Fornecedor))
            {
                return BadRequest("Erro! CNPJ inválido!");
            }
            #endregion Fim validações

            Produtos produto = new Produtos();
            produto.DsProduto = produtoView.DescricaoProduto;
            produto.DtFabricacao = produtoView.DataFabricacao;
            produto.DtValidade = produtoView.DataValidade;
            produto.CodFornecedor = produtoView.CodFornecedor;
            produto.DsFornecedor = produtoView.DescricaoFornecedor;
            produto.CNPJ_Fornecedor = produtoView.CNPJ_Fornecedor;

            produto.DtCriacao = DateTime.Now;
            produto.Ativo = true;
            produto.DtEdicao = null;
            produto.DtExclusao = null;

            await _IAplicacaoProduto.AdicionarProduto(produto);
            return Ok(produto);
        }

        [Produces("application/json")]
        [HttpPut("/api/EditarProduto")]
        public async Task<ActionResult> EditarProduto(int CodProduto, [FromBody] ProdutoModelView produtoView)
        {
            #region Validações
            DateTime dataVazia = new DateTime(1, 1, 1, 0, 0, 0);

            var produtoEdicao = await _IAplicacaoProduto.BuscaPorCod(CodProduto);

            if (produtoEdicao == null)
            {
                return BadRequest("Erro! Produto não encontrado!");
            }

            if (!string.IsNullOrWhiteSpace(produtoView.DescricaoProduto))
            {
                produtoEdicao.DsProduto = produtoView.DescricaoProduto;
            }
            if (produtoView.DataFabricacao != dataVazia && produtoView.DataFabricacao != produtoEdicao.DtFabricacao)
            {
                produtoEdicao.DtFabricacao = produtoView.DataFabricacao;
            }
            if (produtoView.DataValidade != dataVazia && produtoView.DataValidade != produtoEdicao.DtValidade)
            {
                produtoEdicao.DtValidade = produtoView.DataValidade;
            }
            if (!string.IsNullOrWhiteSpace(produtoView.CodFornecedor))
            {
                produtoEdicao.CodFornecedor = produtoView.CodFornecedor;
            }
            if (!string.IsNullOrWhiteSpace(produtoView.DescricaoFornecedor))
            {
                produtoEdicao.DsFornecedor = produtoView.DescricaoFornecedor;
            }
            if (!string.IsNullOrWhiteSpace(produtoView.CNPJ_Fornecedor))
            {
                produtoEdicao.CNPJ_Fornecedor = produtoView.CNPJ_Fornecedor;
            }
            #endregion Fim validações

            produtoEdicao.DtEdicao = DateTime.Now;

            if (produtoEdicao.DtFabricacao >= produtoEdicao.DtValidade)
            {
                return BadRequest("Erro! A data de fabricação não pode ser menor ou igual a data de validade do produto!");
            }
            if (!Validar_CNPJ(produtoEdicao.CNPJ_Fornecedor))
            {
                return BadRequest("Erro! CNPJ inválido!");
            }

            await _IAplicacaoProduto.AtualizaProduto(produtoEdicao);
            return Ok(produtoEdicao);
        }

        [Produces("application/json")]
        [HttpDelete("/api/ExcluirProduto")]
        public async Task<ActionResult> ExcluirProduto(int CodProduto)
        {
            var produtoExclusao = await _IAplicacaoProduto.BuscaPorCod(CodProduto);

            if (produtoExclusao == null)
            {
                return BadRequest("Erro! Produto não encontrado!");
            }

            produtoExclusao.DtExclusao = DateTime.Now;
            produtoExclusao.Ativo = false;

            await _IAplicacaoProduto.AtualizaProduto(produtoExclusao);
            return Ok(produtoExclusao);
        }

        public static bool Validar_CNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
