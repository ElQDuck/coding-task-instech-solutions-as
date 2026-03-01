namespace Claims.BusinessLogic.Interfaces;

/// <summary>
/// Defines the service for auditing claims and covers.
/// </summary>
public interface IAuditerService
{
    /// <summary>
    /// Audits a claim operation.
    /// </summary>
    /// <param name="id">The ID of the claim.</param>
    /// <param name="httpRequestType">The type of HTTP request (e.g., POST, DELETE).</param>
    void AuditClaim(string id, string httpRequestType);

    /// <summary>
    /// Audits a cover operation.
    /// </summary>
    /// <param name="id">The ID of the cover.</param>
    /// <param name="httpRequestType">The type of HTTP request (e.g., POST, DELETE).</param>
    void AuditCover(string id, string httpRequestType);
}
