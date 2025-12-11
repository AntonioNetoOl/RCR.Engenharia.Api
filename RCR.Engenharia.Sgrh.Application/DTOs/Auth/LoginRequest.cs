namespace RCR.Engenharia.Sgrh.Application.DTOs.Auth
{
    public record LoginRequest(
        string Login,
        string Senha,
        string TipoLogin // Novo campo para indicar se é "login" (padrão) ou "cpf"
    );
}