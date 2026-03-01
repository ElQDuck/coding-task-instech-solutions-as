namespace Claims.Database.Auditing;

/// <summary>
/// Defines the interface for the audit message channel.
/// </summary>
public interface IAuditChannel
{
    /// <summary>
    /// Sends an audit message asynchronously.
    /// </summary>
    /// <param name="auditMessage">The audit message to send.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask SendAsync(object auditMessage);

    /// <summary>
    /// Reads all audit messages from the channel.
    /// </summary>
    /// <returns>An async enumerable of audit messages.</returns>
    IAsyncEnumerable<object> ReadAllAsync();
}