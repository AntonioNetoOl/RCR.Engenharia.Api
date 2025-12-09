using RCR.Engenharia.Sgrh.Application.Funcionarios;
using RCR.Engenharia.Sgrh.Domain.Entities;
using RCR.Engenharia.Sgrh.Domain.Interfaces;
using RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCR.Engenharia.Sgrh.Application.Funcionarios
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FuncionarioService(
            IFuncionarioRepository funcionarioRepository,
            IUnitOfWork unitOfWork)
        {
            _funcionarioRepository = funcionarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FuncionarioDto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _funcionarioRepository.ObterPorIdAsync(id, cancellationToken);

            return entity is null ? null : MapToDto(entity);
        }

        public async Task<IReadOnlyList<FuncionarioDto>> ListarAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _funcionarioRepository.ListarAsync(cancellationToken);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<Guid> CriarAsync(FuncionarioDto dto, CancellationToken cancellationToken = default)
        {
            var funcionario = new Funcionario(
                nome: dto.Nome,
                cargo: dto.Cargo,
                dataNascimento: dto.DataNascimento,
                ativo: dto.Ativo
            );

            await _funcionarioRepository.AdicionarAsync(funcionario, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return funcionario.Id;
        }


        public async Task AtualizarAsync(Guid id, FuncionarioDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _funcionarioRepository.ObterPorIdAsync(id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Funcionário não encontrado.");

            entity.Atualizar(
                nome: dto.Nome,
                cargo: dto.Cargo,
                dataNascimento: dto.DataNascimento,
                ativo: dto.Ativo
            );

            _funcionarioRepository.Atualizar(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }


        public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _funcionarioRepository.ObterPorIdAsync(id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Funcionário não encontrado.");

            _funcionarioRepository.Remover(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        private static FuncionarioDto MapToDto(Funcionario entity) =>
         new()
         {
        Id = entity.Id,
        Nome = entity.Nome,
        Cargo = entity.Cargo,
        DataNascimento = entity.DataNascimento,
        Ativo = entity.Ativo
         };

    }
}
