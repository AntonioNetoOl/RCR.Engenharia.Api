using RCR.Engenharia.Sgrh.Domain.Entities;
using RCR.Engenharia.Sgrh.Domain.Interfaces;
using RCR.Engenharia.Sgrh.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            // Aqui delegamos as validações de obrigatoriedade para o domínio.
            var funcionario = new Funcionario(
                nome: dto.Nome,
                rg: dto.Rg,
                cpf: dto.Cpf,
                celular: dto.Celular,
                email: dto.Email,
                dataNascimento: dto.DataNascimento,
                cargo: dto.Cargo,
                dataAdmissao: dto.DataAdmissao,
                salario: dto.Salario,
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
                rg: dto.Rg,
                cpf: dto.Cpf,
                celular: dto.Celular,
                email: dto.Email,
                dataNascimento: dto.DataNascimento,
                cargo: dto.Cargo,
                dataAdmissao: dto.DataAdmissao,
                salario: dto.Salario,
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

        /// <summary>
        /// Atualiza a foto do funcionário.
        /// </summary>
        public async Task AtualizarFotoAsync(Guid id, byte[] foto, CancellationToken cancellationToken = default)
        {
            var entity = await _funcionarioRepository.ObterPorIdAsync(id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Funcionário não encontrado.");

            entity.DefinirFoto(foto);

            _funcionarioRepository.Atualizar(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        /// <summary>
        /// Remove a foto do funcionário.
        /// </summary>
        public async Task RemoverFotoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _funcionarioRepository.ObterPorIdAsync(id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Funcionário não encontrado.");

            entity.RemoverFoto();

            _funcionarioRepository.Atualizar(entity);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        private static FuncionarioDto MapToDto(Funcionario entity) =>
            new()
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cargo = entity.Cargo,
                Rg = entity.Rg,
                Cpf = entity.Cpf,
                Celular = entity.Celular,
                Email = entity.Email,
                DataNascimento = entity.DataNascimento,
                DataAdmissao = entity.DataAdmissao,
                Salario = entity.Salario,
                Foto = entity.Foto,
                Ativo = entity.Ativo
            };
    }
}
