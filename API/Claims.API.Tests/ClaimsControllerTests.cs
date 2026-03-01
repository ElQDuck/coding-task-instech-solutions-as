
using Claims.API.Controllers;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Claims.Tests
{
    [Category("UnitTests")]
    public class ClaimsControllerTests
    {
        private ILogger<ClaimsController> _loggerMock;
        private IClaimsService _claimsServiceMock;
        private ClaimsController _testee;

        [SetUp]
        public void Setup()
        {
            _loggerMock = Substitute.For<ILogger<ClaimsController>>();
            _claimsServiceMock = Substitute.For<IClaimsService>();
            _testee = new ClaimsController(_loggerMock, _claimsServiceMock);
        }

        [TearDown]
        public void TearDown() { }

        [Test]
        public async Task ClaimsController_GetClaimsAsync_Success()
        {
            // Prepare
            var returnValue = Task.FromResult(Result.FromSuccess(new List<Claim>().AsEnumerable()));
            _claimsServiceMock.GetClaimsAsync().Returns(returnValue);

            // Act
            var result = await _testee.GetClaimsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = (OkObjectResult)result;
            var realValue = (IEnumerable<Claim>)ok.Value!;
            Assert.That(realValue.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task ClaimsController_GetClaimAsync_Success()
        {
            // Prepare
            var claim = new Claim { Id = "c1" };
            _claimsServiceMock.GetClaimAsync("c1").Returns(Task.FromResult(Result.FromSuccess(claim)));

            // Act
            var result = await _testee.GetClaimAsync("c1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = (OkObjectResult)result;
            var realValue = (Claim)ok.Value!;
            Assert.That(realValue.Id, Is.EqualTo("c1"));
        }

        [Test]
        public async Task ClaimsController_CreateClaimAsync_Success()
        {
            // Prepare
            var claim = new Claim { Id = "create-1" };
            _claimsServiceMock.CreateClaimAsync(claim).Returns(Task.FromResult(Result.FromSuccess(claim)));

            // Act
            var result = await _testee.CreateClaimAsync(claim);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = (OkObjectResult)result;
            var realValue = (Claim)ok.Value!;
            Assert.That(realValue.Id, Is.EqualTo("create-1"));
        }

        [Test]
        public async Task ClaimsController_DeleteClaimAsync_Success()
        {
            // Prepare
            _claimsServiceMock.DeleteClaimAsync("d1").Returns(Task.FromResult(Result.FromSuccess()));

            // Act
            var result = await _testee.DeleteClaimAsync("d1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }
}
