namespace Claims.Database.Auditing;

// Task 3: Use this interface to decouple the API from the DB write
public interface IAuditChannel
{
    ValueTask SendAsync(object auditMessage);
    IAsyncEnumerable<object> ReadAllAsync();
}