namespace Application.Features.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Method to start querying the database
    /// </summary>
    /// <returns>Non tracking collection of projection for querying</returns>
    public IQueryable<TEntity> Query();

    /// <summary>
    ///     Creates a new projection instance in the database
    /// </summary>
    /// <param name="entity">A projection to create</param>
    public Task Create(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an existing projection instance in the database by projection id. Projection should exist in the database
    /// </summary>
    /// <param name="entity">A projection to update</param>
    /// <param name="cancellationToken"></param>
    public Task Update(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Reads an existing projection and returns it as non tracking
    /// </summary>
    /// <param name="entityId">The id of a projection to read</param>
    /// <returns>A tracking projection instance or null</returns>
    public Task<TEntity?> Read(object[] entityId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes an existing projection by id
    /// </summary>
    /// <param name="entityId">The id of a projection to delete</param>
    public Task Delete(object[] entityId, CancellationToken cancellationToken = default);
}