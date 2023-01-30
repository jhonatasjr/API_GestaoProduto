using Dominio.Interfaces.Genericos;
using Entidades;
using Infraestrutura.Configuracoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio.Generico
{
    public class RepositorioGenerico<T> : IGenericos<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextoBase> _OptionsBuilder;

        public RepositorioGenerico()
        {
            _OptionsBuilder = new DbContextOptions<ContextoBase>();
        }

        public async Task Adicionar(T Objeto)
        {
            using (var data = new ContextoBase(_OptionsBuilder))
            {
                try
                {
                    await data.Set<T>().AddAsync(Objeto);
                    await data.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task Atualizar(T Objeto)
        {
            using (var data = new ContextoBase(_OptionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> BuscaPorCod(int cod)
        {
            using (var data = new ContextoBase(_OptionsBuilder))
            {
                try
                {
                    return await data.Set<T>().FindAsync(cod);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task Excluir(T Objeto)
        {
            using (var data = new ContextoBase(_OptionsBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<T>> Listar()
        {
            using (var data = new ContextoBase(_OptionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produtos>> ListarProdutos(Expression<Func<Produtos, bool>> exProduto)
        {
            using (var banco = new ContextoBase(_OptionsBuilder))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }




        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }


        #endregion
    }
}
