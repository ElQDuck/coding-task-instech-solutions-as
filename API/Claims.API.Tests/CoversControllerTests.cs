
using Claims.API.Controllers;
using Claims.BusinessLogic.Entities;
using Claims.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Claims.Tests
{
    /// <summary>
    /// Implementation of the UnitTests for the Covers Controller.
    /// </summary>
    [Category("UnitTests")]
    public class CoversControllerTests
    {
        private ILogger<CoversController> _loggerMock;
        private IPremiumComputeService _premiumComputeServiceMock;
        private ICoversService _coversServiceMock;
        private CoversController _testee;
        
        [SetUp]
        public void Setup()
        {
            _loggerMock = Substitute.For<ILogger<CoversController>>();
            _premiumComputeServiceMock = Substitute.For<IPremiumComputeService>();
            _coversServiceMock = Substitute.For<ICoversService>();
            _testee = new CoversController(_coversServiceMock, _premiumComputeServiceMock, _loggerMock );
        }
        
        [TearDown]
        public void TearDown(){}

        /// <summary>
        /// Test the GetCoversAsync route for the happy path.
        /// </summary>
        [Test]
        public async Task CoversController_GetCoversAsync_Success()
        {
            // Prepare
            var returnValue = Task.FromResult(Result.FromSuccess(new List<Cover>().AsEnumerable()));
            _coversServiceMock.GetCoversAsync().Returns(returnValue);
            
            // Act
            var result = await _testee.GetCoversAsync();
            
            // Assert
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            // Now that we checked the right instance of the result
            // we can be sure that casting is safe.
            var okObjectResult = (OkObjectResult)result.Result!;
            var realValue = (IEnumerable<Cover>)okObjectResult.Value!;
            Assert.That(realValue!.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task CoversController_GetCoverAsync_Success()
        {
            // Prepare
            var cover = new Cover { Id = "1" };
            var returnValue = Task.FromResult(Result.FromSuccess(cover));
            _coversServiceMock.GetCoverAsync("1").Returns(returnValue);

            // Act
            var result = await _testee.GetCoverAsync("1");

            // Assert
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result.Result!;
            var realValue = (Cover)okObjectResult.Value!;
            Assert.That(realValue.Id, Is.EqualTo("1"));
        }

        [Test]
        public async Task CoversController_CreateCoverAsync_Success()
        {
            // Prepare
            var cover = new Cover { Id = "create-id" };
            var returnValue = Task.FromResult(Result.FromSuccess(cover));
            _coversServiceMock.CreateCoverAsync(cover).Returns(returnValue);

            // Act
            var result = await _testee.CreateCoverAsync(cover);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okObjectResult = (OkObjectResult)result;
            var realValue = (Cover)okObjectResult.Value!;
            Assert.That(realValue.Id, Is.EqualTo("create-id"));
        }

        [Test]
        public async Task CoversController_DeleteCoverAsync_Success()
        {
            // Prepare
            _coversServiceMock.DeleteCoverAsync("del-1").Returns(Task.FromResult(Result.FromSuccess()));

            // Act
            var result = await _testee.DeleteCoverAsync("del-1");

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void CoversController_ComputePremium_Success()
        {
            // Prepare
            var start = DateTime.UtcNow;
            var end = start.AddDays(1);
            _premiumComputeServiceMock.ComputePremium(start, end, CoverType.Tanker).Returns(12.34m);

            // Act
            var result = _testee.ComputePremiumAsync(start, end, CoverType.Tanker);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var ok = (OkObjectResult)result;
            Assert.That(ok.Value, Is.EqualTo(12.34m));
        }

    }
}
