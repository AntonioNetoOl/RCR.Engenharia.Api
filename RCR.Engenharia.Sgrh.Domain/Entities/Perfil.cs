using System;
using System.Collections.Generic;

namespace RCR.Engenharia.Sgrh.Domain.Entities
{
    public class Perfil
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } // Ex: "Administrador"
        public string? Descricao { get; private set; }
        public bool EhAdmin { get; private set; } // Se true, acessa tudo

        // Relacionamentos
        public virtual ICollection<Permissao> Permissoes { get; private set; } = new List<Permissao>();
        public virtual ICollection<Usuario> Usuarios { get; private set; } = new List<Usuario>();

        private Perfil() { }

        public Perfil(string nome, string? descricao, bool ehAdmin = false)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            EhAdmin = ehAdmin;
        }

        public void AdicionarPermissao(Permissao permissao)
        {
            Permissoes.Add(permissao);
        }
    }
}