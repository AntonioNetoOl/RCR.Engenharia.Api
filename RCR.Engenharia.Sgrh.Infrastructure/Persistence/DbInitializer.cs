using Microsoft.EntityFrameworkCore;
using RCR.Engenharia.Sgrh.Domain.Entities;
using RCR.Engenharia.Sgrh.Infrastructure.Auth;
using RCR.Engenharia.Sgrh.Infrastructure.Persistence.Context;

namespace RCR.Engenharia.Sgrh.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        // O método é Async porque operações em banco de dados podem demorar
        public static async Task InitializeAsync(SgrhDbContext context)
        {
            // ==============================================================================
            // PASSO 1: APLICAR MIGRATIONS (Os "Blueprints" da Construção)
            // ==============================================================================
            // Isso garante que, se o banco não existir, ele será criado.
            // Se o banco existir mas estiver desatualizado (ex: faltar uma coluna nova),
            // ele aplica as mudanças pendentes automaticamente.
            await context.Database.MigrateAsync();

            // Instancia o serviço de hash manualmente para usar no Seed
            var passwordHasher = new PasswordHashService();

            // ==============================================================================
            // PASSO 2: A FONTE DA VERDADE (O Catálogo de Menus)
            // ==============================================================================
            // Aqui definimos HARDCODED (fixo no código) quais menus o sistema suporta.
            // Pense nisso como o cardápio do restaurante. Se não está aqui, a cozinha não faz.
            // Se você criar uma tela nova no Front, é AQUI que você adiciona a linha nova.
            var permissoesDoSistema = new List<Permissao>
            {
                // --- Grupo PONTO (Coincidindo com o data-tab="tabPonto" do HTML) ---
                new Permissao("Menu: Cartão de Ponto", "MENU.PONTO.CARTAO", "Ponto"),
                new Permissao("Menu: Pontos Justificados", "MENU.PONTO.JUSTIFICATIVA", "Ponto"),
                new Permissao("Menu: Incluir Ponto", "MENU.PONTO.INCLUIR", "Ponto"),

                // --- Grupo DOCUMENTOS ---
                new Permissao("Menu: Documentos", "MENU.DOCUMENTOS", "Documentos"),

                // --- Grupo FUNCIONÁRIOS ---
                new Permissao("Menu: Gestão Funcionários", "MENU.FUNCIONARIOS.GESTAO", "Funcionarios"),
                new Permissao("Menu: Segurança Trabalho", "MENU.FUNCIONARIOS.SEGURANCA", "Funcionarios"),

                // --- Grupo BENEFÍCIOS ---
                new Permissao("Menu: Vale Transporte", "MENU.BENEFICIOS.VT", "Beneficios"),
                new Permissao("Menu: Vale Refeição", "MENU.BENEFICIOS.VR", "Beneficios")
            };

            // ==============================================================================
            // PASSO 3: A SINCRONIZAÇÃO INTELIGENTE (Code First -> DB)
            // ==============================================================================
            // Buscamos o que JÁ existe no banco para comparar.
            var permissoesNoBanco = await context.Permissoes.ToListAsync();

            // Lista auxiliar para sabermos quais são as novidades desta versão
            var novasPermissoes = new List<Permissao>();

            foreach (var permCodigo in permissoesDoSistema)
            {
                // LÓGICA DE PROTEÇÃO:
                // Só adicionamos se NÃO existir nenhum registro com esse mesmo Slug.
                // Isso evita duplicar "MENU.PONTO.CARTAO" toda vez que o sistema reinicia.
                if (!permissoesNoBanco.Any(p => p.Slug == permCodigo.Slug))
                {
                    context.Permissoes.Add(permCodigo);
                    novasPermissoes.Add(permCodigo); // Guardamos na lista de novidades
                }
            }

            // Salvamos as novas permissões (se houver alguma)
            await context.SaveChangesAsync();

            // ==============================================================================
            // PASSO 4: O PERFIL "MASTER" (O Chefe Supremo)
            // ==============================================================================
            // O sistema precisa de pelo menos um perfil que possa fazer tudo.
            // A flag 'EhAdmin' serve para dizer ao sistema: "Não cheque permissões, libere tudo".
            var perfilMaster = await context.Perfis
                .Include(p => p.Permissoes) // Precisamos carregar as permissões dele para verificar abaixo
                .FirstOrDefaultAsync(p => p.EhAdmin == true);

            if (perfilMaster == null)
            {
                perfilMaster = new Perfil("Master", "Super Admin do Sistema", ehAdmin: true);
                context.Perfis.Add(perfilMaster);
            }




            // ==============================================================================
            // PASSO 5: AUTO-VINCULAR NOVIDADES AO CHEFE
            // ==============================================================================
            // CENÁRIO: Você acabou de criar o menu "Férias" e subiu para produção.
            // SEM ISSO: O Admin loga e não vê o menu, e tem que ir no banco se dar permissão.
            // COM ISSO: O sistema detecta que "Férias" é novo e já entrega a chave pro Admin.
            if (novasPermissoes.Any())
            {
                foreach (var novaPerm in novasPermissoes)
                {
                    // Verifica se ele já tem (dupla checagem de segurança)
                    if (!perfilMaster.Permissoes.Any(p => p.Slug == novaPerm.Slug))
                    {
                        perfilMaster.AdicionarPermissao(novaPerm);
                    }
                }
            }

            // ==============================================================================
            // PASSO X: O PERFIL "COLABORADOR" E SUAS PERMISSÕES
            // ==============================================================================

            // 1. Busca ou Cria o Perfil Colaborador
            var perfilColaborador = await context.Perfis
                .Include(p => p.Permissoes) // Importante trazer as permissões existentes
                .FirstOrDefaultAsync(p => p.Nome == "Colaborador");

            if (perfilColaborador == null)
            {
                // Criação do perfil se não existir
                // Nota: Ajuste os parâmetros conforme o construtor do seu Perfil.cs
                perfilColaborador = new Perfil("Colaborador", "Acesso padrão aos recursos de RH", ehAdmin: false);
                context.Perfis.Add(perfilColaborador);

                // Salvamos aqui para garantir que o Perfil ganhe um ID antes de vincular permissões
                await context.SaveChangesAsync();
            }

            // 2. Definir quais Slugs (códigos de menu) o Colaborador DEVE ter
            var slugsPermitidosColaborador = new List<string>
            {
                "MENU.PONTO.CARTAO",
                "MENU.PONTO.JUSTIFICATIVA",
                "MENU.PONTO.INCLUIR",
                "MENU.DOCUMENTOS",
                "MENU.BENEFICIOS.VT",
                "MENU.BENEFICIOS.VR"
                // Note que NÃO incluímos "MENU.FUNCIONARIOS.GESTAO" aqui
            };

            // 3. Buscar os objetos de Permissao no banco baseados nos slugs acima
            // (Assumimos que as permissões já foram cadastradas no PASSO 2 do seu código anterior)
            var permissoesParaVincular = await context.Permissoes
                .Where(p => slugsPermitidosColaborador.Contains(p.Slug))
                .ToListAsync();

            // 4. Vincular as permissões ao perfil (Preencher a tabela PerfisPermissoes)
            bool houveAlteracaoColaborador = false;

            foreach (var perm in permissoesParaVincular)
            {
                // Só adiciona se o perfil ainda não tiver essa permissão
                if (!perfilColaborador.Permissoes.Any(p => p.Id == perm.Id))
                {
                    // Se você tiver o método auxiliar 'AdicionarPermissao' na classe Perfil:
                    // perfilColaborador.AdicionarPermissao(perm);

                    // CASO CONTRÁRIO (uso padrão de ICollection):
                    perfilColaborador.Permissoes.Add(perm);

                    houveAlteracaoColaborador = true;
                }
            }

            if (houveAlteracaoColaborador)
            {
                await context.SaveChangesAsync();
            }

            // ==============================================================================
            // PASSO 6: O PRIMEIRO USUÁRIO (Para você conseguir logar)
            // ==============================================================================
            // Verifica se existe alguém com o email do admin.
            if (!await context.Usuarios.AnyAsync(u => u.Email == "admin@rcr.com.br"))
            {
                // 1. Cria um objeto temporário apenas para gerar o hash (o hasher pede o objeto usuario)
                var adminTemp = new Usuario("Admin", "admin@rcr.com.br", "", Guid.Empty, null, "admin");

                // 2. Gera o Hash da senha "123456"
                var senhaHashAdmin = passwordHasher.HashSenha(adminTemp, "123456");

                // 3. Cria o usuário definitivo com o Hash
                var adminUser = new Usuario(
                    "Admin",
                    "admin@rcr.com.br",
                    senhaHashAdmin, // <--- Hash aqui
                    perfilMaster.Id, // Certifique-se que a var perfilMaster existe nesse escopo
                    null,
                    "admin"
                );

                context.Usuarios.Add(adminUser);
            }



            // Só cria o usuário se o perfil existir
            if (perfilColaborador != null && !await context.Usuarios.AnyAsync(u => u.Email == "joao@rcr.com.br"))
            {
                var joaoTemp = new Usuario("João Silva", "joao@rcr.com.br", "", Guid.Empty, null, "joao");

                // Gera Hash da senha "123"
                var senhaHashJoao = passwordHasher.HashSenha(joaoTemp, "123");

                var userJoao = new Usuario(
                    "João Silva",
                    "joao@rcr.com.br",
                    senhaHashJoao, // <--- Hash aqui
                    perfilColaborador.Id,
                    "11122233344",
                    "joao"
                );

                context.Usuarios.Add(userJoao);
            }

            // Salva todas as alterações finais (Perfil e Usuário)
            await context.SaveChangesAsync();
        }
    }
}