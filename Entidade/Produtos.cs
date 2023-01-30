using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("Produtos")]
    //Erdando a classe notifica, que é a classe pra retorna todas as mensagens de validações
    public class Produtos
    {
        [Key]
        [Column("CodProduto")]
        public int CodProduto { get; set; }
        [Column("DsProduto")]
        public string DsProduto { get; set; }

        public DateTime DtFabricacao { get; set; }

        public DateTime DtValidade { get; set; }

        [Column("CodFornecedor")]
        public string CodFornecedor { get; set; }
        [Column("DsFornecedor")]
        public string DsFornecedor { get; set; }
        [Column("CNPJ_Fornecedor")]
        public string CNPJ_Fornecedor { get; set; }
        [Column("Ativo")]
        public bool Ativo { get; set; }
        [Column("DtCriacao")]
        public DateTime? DtCriacao { get; set; }
        [Column("DtEdicao")]
        public DateTime? DtEdicao { get; set; }

        [Column("DtExclusao")]
        public DateTime? DtExclusao { get; set; }
    }
}
