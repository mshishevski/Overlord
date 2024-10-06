using Microsoft.EntityFrameworkCore;
using Overlord.Domain.Entities;

namespace Overlord.Application
{
    public interface IOverlordContext
    {
        DbSet<Metric> Metrics { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
