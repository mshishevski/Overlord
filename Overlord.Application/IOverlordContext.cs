using Microsoft.EntityFrameworkCore;

using Overlord.Domain.Entities;

namespace Overlord.Application
{
    public interface IOverlordContext
    {
        DbSet<Metric> Metrics { get; }
        DbSet<Client> Clients { get; }
        DbSet<Room> Rooms { get; }
        DbSet<Floor> Floors { get; }
        DbSet<Building> Buildings { get; }
        DbSet<MetricType> MetricTypes { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
