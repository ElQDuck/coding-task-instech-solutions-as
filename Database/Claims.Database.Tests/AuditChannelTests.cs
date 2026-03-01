using Claims.Database.Auditing;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Claims.Database.Tests;

[Category("UnitTests")]
public class AuditChannelTests
{
    [Test]
    public async Task AuditChannel_SendAndRead_Works()
    {
        var logger = Substitute.For<ILogger<AuditChannel>>();
        var channel = new AuditChannel(logger);

        await channel.SendAsync("msg1");

        var enumerator = channel.ReadAllAsync().GetAsyncEnumerator();
        try
        {
            var has = await enumerator.MoveNextAsync();
            Assert.That(has, Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("msg1"));
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
    }
}
