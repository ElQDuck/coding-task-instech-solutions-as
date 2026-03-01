using System.Threading.Channels;

namespace Claims.Database.Auditing
{
    /// <summary>
    /// Implements an asynchronous channel for auditing messages.
    /// </summary>
    public class AuditChannel : IAuditChannel
    {
        private readonly Channel<object> _channel;

        /// <summary>
        /// Initializes an instance of the <see cref="AuditChannel"/> class.
        /// </summary>
        public AuditChannel()
        {
            // Unbounded channel for simplicity, could be bounded in production
            _channel = Channel.CreateUnbounded<object>();
        }

        /// <inheritdoc/>
        public ValueTask SendAsync(object auditMessage) => _channel.Writer.WriteAsync(auditMessage);

        /// <inheritdoc/>
        public IAsyncEnumerable<object> ReadAllAsync() => _channel.Reader.ReadAllAsync();
    }
}
