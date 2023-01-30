using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class ProdutoModelView
    {
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string CodFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJ_Fornecedor { get; set; }
    }
}
