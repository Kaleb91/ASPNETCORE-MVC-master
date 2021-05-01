using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Interfaces.Entidades;
using Cooperchip.ITDeveloper.Domain.Models;
using Cooperchip.ITDeveloper.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Application.Servicos
{
    public class PacienteService : RepositoryGeneric<Paciente, Guid>, IRepositoryDomainPaciente
    {
        private readonly ITDeveloperDbContext _ctx;
        public PacienteService(ITDeveloperDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Paciente>> ListaPacientes()
        {
            return await _ctx.Paciente.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> ListaPacientesComEstado()
        {
            return await _ctx.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EstadoPaciente>> ListaEstadoPaciente()
        {
            return await _ctx.EstadoPaciente.AsNoTracking().ToListAsync();
        }

        public async Task<Paciente> ObterPacienteComEstadoPaciente(Guid PacienteId)
        {
            return await _ctx.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().FirstOrDefaultAsync(p => p.Id == PacienteId);
        }
    }
}
