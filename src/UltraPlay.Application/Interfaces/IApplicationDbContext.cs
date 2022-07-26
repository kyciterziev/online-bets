using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Sport> Sports { get; }
        DbSet<Event> Events { get; }
        DbSet<Match> Matches { get; }
        DbSet<Bet> Bets { get; }
        DbSet<Odd> Odds { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}