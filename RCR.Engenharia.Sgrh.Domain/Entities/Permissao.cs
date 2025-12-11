using System;
using System.Collections.Generic;

namespace RCR.Engenharia.Sgrh.Domain.Entities
{
    public class Permissao
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } // Ex: "Visualizar Ponto"
        public string Slug { get; private set; } // Ex: "MENU.PONTO"
        public string Grupo { get; private set; } // Ex: "Ponto"

        // Relacionamento (Uma permissão pode estar em vários perfis)
        public virtual ICollection<Perfil> Perfis { get; private set; }

        private Permissao() { } // Necessário para o Entity Framework

        public Permissao(string nome, string slug, string grupo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Slug = slug;
            Grupo = grupo;
        }
    }
}