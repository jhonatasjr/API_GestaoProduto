using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Infraestrutura.Configuracoes
{
    public class ContextoBase : DbContext
    {
        public ContextoBase(DbContextOptions<ContextoBase> options) : base(options)
        {

        }

        public DbSet<Produtos> Produtos { get; set; }

        //Metodo que verifica se a string de conexao nao foi configurada ele configura
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string ObterStringConexao()
        {
            return "Data Source=DESKTOP-6DFO69K;Initial Catalog=API_GestaoProduto;Integrated Security=True";
        }
    }
}
