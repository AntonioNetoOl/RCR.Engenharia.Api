using System.Collections.Generic;

namespace RCR.Engenharia.Sgrh.Application.DTOs.Auth
{
    public record LoginResponse(
        string Token,
        string NomeUsuario,
        string Email,
        string Perfil,
        List<string> Permissoes
    );
}