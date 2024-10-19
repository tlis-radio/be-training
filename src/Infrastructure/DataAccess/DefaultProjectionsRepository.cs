using Application.Features;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class DefaultProjectionsRepository<TProjection>(DbContext dbContext) : IProjectionsRepository<TProjection>
    where TProjection : class, IProjection
{
    public virtual IQueryable<TProjection> Query() => dbContext.Set<TProjection>().AsNoTracking();

    public virtual async Task Create(TProjection projection, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TProjection>().AddAsync(projection, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Update(TProjection projection, CancellationToken cancellationToken = default)
    {
        dbContext.Set<TProjection>().Update(projection);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TProjection?> Read(object[] projectionId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TProjection>().FindAsync(projectionId, cancellationToken: cancellationToken);
    }

    public virtual async Task Delete(object[] projectionId, CancellationToken cancellationToken = default)
    {
        TProjection? projection = await Read(projectionId, cancellationToken);
        
        if (projection is null) return;
        
        dbContext.Set<TProjection>().Remove(projection);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}