using Dominio.Interfaces;
using Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio.Generico;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio.Repositorios
{
    public class RepositorioProduto : RepositorioGenerico<Produtos>, IProduto
    {
        private readonly DbContextOptions<ContextoBase> _OptionsBuilder;

        public RepositorioProduto()
        {
            _OptionsBuilder = new DbContextOptions<ContextoBase>();
        }

        public async Task<List<Produtos>> ListarProdutos(Expression<Func<Produtos, bool>> exProduto)
        {
            using (var banco = new ContextoBase(_OptionsBuilder))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }
    }
}
