namespace Application.Features;

public interface IProjectionsRepository<TProjection> where TProjection : IProjection
{
    /// <summary>
    ///     Method to start querying the database
    /// </summary>
    /// <returns>Non tracking collection of projection for querying</returns>
    public IQueryable<TProjection> Query();

    /// <summary>
    ///     Creates a new projection instance in the database
    /// </summary>
    /// <param name="projection">A projection to create</param>
    public Task Create(TProjection projection, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates an existing projection instance in the database by projection id. Projection should exist in the database
    /// </summary>
    /// <param name="projection">A projection to update</param>
    /// <param name="cancellationToken"></param>
    public Task Update(TProjection projection, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Reads an existing projection and returns it as non tracking
    /// </summary>
    /// <param name="projectionId">The id of a projection to read</param>
    /// <returns>A tracking projection instance or null</returns>
    public Task<TProjection?> Read(object[] projectionId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes an existing projection by id
    /// </summary>
    /// <param name="projectionId">The id of a projection to delete</param>
    public Task Delete(object[] projectionId, CancellationToken cancellationToken = default);
}