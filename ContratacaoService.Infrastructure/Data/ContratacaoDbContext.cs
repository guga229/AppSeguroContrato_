using ContratacaoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratacaoService.Infrastructure.Data
{
    public class ContratacaoDbContext : DbContext
    {
        public ContratacaoDbContext(DbContextOptions<ContratacaoDbContext> options) : base(options) { }

        public DbSet<Contrato> Contratos { get; set; }
    }

}
