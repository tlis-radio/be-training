using Application.Features.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class DefaultRepository<TProjection>(DbContext dbContext) : IRepository<TProjection>
    where TProjection : class
{
    public virtual IQueryable<TProjection> Query() => dbContext.Set<TProjection>().AsNoTracking();

    public virtual async Task Create(TProjection entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TProjection>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Update(TProjection entity, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TProjection>().Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TProjection?> Read(object[] entityId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TProjection>().FindAsync(entityId, cancellationToken: cancellationToken);
    }

    public virtual async Task Delete(object[] entityId, CancellationToken cancellationToken = default)
    {
        TProjection? projection = await Read(entityId, cancellationToken);
        
        if (projection is null) return;
        
        dbContext.Set<TProjection>().Remove(projection);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}