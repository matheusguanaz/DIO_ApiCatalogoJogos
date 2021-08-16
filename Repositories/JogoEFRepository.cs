using ApiCatalogoJogos.Db;
using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoEFRepository : IJogoRepository
    {
        private readonly JogoDbContext context;

        public JogoEFRepository(JogoDbContext context)
        {
            this.context = context;
        }
        public Task Atualizar(Guid id, Jogo jogo)
        {
            var jogoBanco = context.Jogo.SingleOrDefault(j => j.Id == id);
            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Produtora = jogo.Produtora;
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Task Excluir(Guid id)
        {
            var jogoARemover = context.Jogo.FirstOrDefault(j => j.Id == id);
            context.Jogo.Remove(jogoARemover);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Inserir(Jogo jogo)
        {
            context.Jogo.Add(jogo);
            context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = context.Jogo.Skip((pagina - 1) * quantidade).Take(quantidade).ToList();
            return Task.FromResult(jogos);
        }

        public Task<Jogo> Obter(Guid id)
        {
            var jogo = context.Jogo.FirstOrDefault(j => j.Id == id);
            return Task.FromResult(jogo);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogo = context.Jogo.Where(j => j.Nome == nome && j.Produtora == produtora).ToList();
            return Task.FromResult(jogo);
        }
    }
}
