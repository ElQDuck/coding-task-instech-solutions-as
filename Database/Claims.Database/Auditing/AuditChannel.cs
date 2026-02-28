using System.Threading.Channels;

namespace Claims.Database.Auditing
{
    public class AuditChannel : IAuditChannel
    {
        private readonly Channel<object> _channel;

        public AuditChannel()
        {
            // Unbounded channel for simplicity, could be bounded in production
            _channel = Channel.CreateUnbounded<object>();
        }

        public ValueTask SendAsync(object auditMessage) => _channel.Writer.WriteAsync(auditMessage);

        public IAsyncEnumerable<object> ReadAllAsync() => _channel.Reader.ReadAllAsync();
    }
}
