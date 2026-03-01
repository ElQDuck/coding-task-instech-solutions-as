using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Claims.Database.Auditing
{
    /// <summary>
    /// Implements an asynchronous channel for auditing messages.
    /// </summary>
    public class AuditChannel : IAuditChannel
    {
        private readonly Channel<object> _channel;
        private readonly ILogger<AuditChannel> _logger;

        /// <summary>
        /// Initializes an instance of the <see cref="AuditChannel"/> class.
        /// </summary>
        public AuditChannel(ILogger<AuditChannel> logger)
        {
            _logger = logger;
            // Unbounded channel for simplicity, could be bounded in production
            _channel = Channel.CreateUnbounded<object>();
            _logger.LogDebug("AuditChannel created. InstanceId={id}", this.GetHashCode());
        }

        /// <inheritdoc/>
        public ValueTask SendAsync(object auditMessage)
        {
            _logger.LogDebug("AuditChannel.SendAsync called. InstanceId={id} MessageType={type}", this.GetHashCode(), auditMessage?.GetType().FullName);
            return _channel.Writer.WriteAsync(auditMessage);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<object> ReadAllAsync() => _channel.Reader.ReadAllAsync();
    }
}
