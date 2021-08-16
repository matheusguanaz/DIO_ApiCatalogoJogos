using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("ebcaf1cd-4f6e-4aad-9cce-9656d19bb87a"), new Jogo {Id = Guid.Parse("ebcaf1cd-4f6e-4aad-9cce-9656d19bb87a"), Nome = "Grand Chase", Produtora = "Kog", Preco = 30} },
            {Guid.Parse("da15258f-d427-4d87-a8b4-d13f433b3da0"), new Jogo {Id = Guid.Parse("da15258f-d427-4d87-a8b4-d13f433b3da0"), Nome = "Bloons TD 6", Produtora = "Ninja Kiwi", Preco = 10} },
            {Guid.Parse("84198ab0-e97d-4d62-8951-3c0ff3a928d4"), new Jogo {Id = Guid.Parse("84198ab0-e97d-4d62-8951-3c0ff3a928d4"), Nome = "CSGO", Produtora = "Valve", Preco = 16} },
            {Guid.Parse("bcf2cad7-8edd-4d28-bf9d-b2a2c210086a"), new Jogo {Id = Guid.Parse("bcf2cad7-8edd-4d28-bf9d-b2a2c210086a"), Nome = "Valorant", Produtora = "Riot", Preco = 0} },
            {Guid.Parse("8a643e90-bddd-4a77-b0bb-4387a96614e3"), new Jogo {Id = Guid.Parse("8a643e90-bddd-4a77-b0bb-4387a96614e3"), Nome = "League of Legends", Produtora = "Kog", Preco = 0} }
        };
        public Task Atualizar(Guid id, Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        public Task Excluir(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach(var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }
            return Task.FromResult(retorno);
        }

       
    }
}
