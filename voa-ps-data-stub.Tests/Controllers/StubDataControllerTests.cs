using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using voa_ps_data_stub.Controllers;
using voa_ps_data_stub.Services.Interfaces;
using Xunit;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace voa_ps_data_stub.Tests.Controllers
{
    public class StubDataControllerTests
    {
        [Fact]
        public void ShouldContainCorrectHeaders()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<string>())).Returns("{ 'name': 'jabed' }");

            string urlPath = "/some-controller/someaction/chk1000";

            HttpContext defaultContext = new DefaultHttpContext();
            defaultContext.Request.Path = urlPath;

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;
            controller.HttpContext.Request.Path = urlPath;

            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result);
            var objResult = (OkObjectResult)result;
            Assert.Equal(200, objResult.StatusCode.Value);
        }

        [Fact]
        public void ShouldContainCorrectData()
        {
            string filePath = "/some-controller/someaction/chk1000.json";
            var dataAccessMock = new Mock<IDataAccessService>();
            string fakeData = "{\r\n  \"name\": \"jabed\"\r\n}";
            dataAccessMock.Setup(x => x.ReadFile(filePath)).Returns(fakeData);

            HttpContext defaultContext = new DefaultHttpContext();
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            string urlPath = "/some-controller/someaction/chk1000";
            controller.HttpContext.Request.Path = urlPath;

            var result = controller.Get();
            Assert.IsType<OkObjectResult>(result);

            var actualData = (OkObjectResult)result;
            Assert.Equal(fakeData, actualData.Value.ToString());
        }

        [Fact]
        public void ShouldReturnNotFoundWhenInvalidURL()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<String>())).Throws(new FileNotFoundException());

            HttpContext defaultContext = new DefaultHttpContext();
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            var result = controller.Get();
            Assert.IsType<NotFoundResult>(result);

            var actualResult = (NotFoundResult)result;
            Assert.Equal(404, actualResult.StatusCode);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenFileDoesNotExist()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<String>())).Throws(new FileNotFoundException());

            HttpContext defaultContext = new DefaultHttpContext();
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            string urlPath = "/some-controller/someaction/chk1000";
            controller.HttpContext.Request.Path = urlPath;

            var result = controller.Get();
            Assert.IsType<NotFoundResult>(result);

            var actualResult = (NotFoundResult)result;
            Assert.Equal(404, actualResult.StatusCode);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenUrlHasInvalidFile()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<String>())).Throws(new FileNotFoundException());

            HttpContext defaultContext = new DefaultHttpContext();
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            string urlPath = "/";
            controller.HttpContext.Request.Path = urlPath;

            var result = controller.Get();
            Assert.IsType<NotFoundResult>(result);

            var actualResult = (NotFoundResult)result;
            Assert.Equal(404, actualResult.StatusCode);
        }
    }
}
