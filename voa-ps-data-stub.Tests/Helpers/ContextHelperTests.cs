using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xunit;
using voa_ps_data_stub.Helpers;
using Microsoft.AspNetCore.Http;
using voa_ps_data_stub.Controllers;
using Microsoft.AspNetCore.Mvc;
using voa_ps_data_stub.Services.Interfaces;
using Moq;
using static voa_ps_data_stub.Helpers.ContextHelper;
using Microsoft.AspNetCore.Hosting;

namespace voa_ps_data_stub.Tests
{
    public class ContextHelperTests
    {
        [Fact]
        public void GetDirectoryAndFilesFromValidURLWithQueryString()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<string>())).Returns("expectedData");

            HttpContext defaultContext = new DefaultHttpContext();
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            string urlPath = "/some-controller/someaction";
            controller.HttpContext.Request.Path = urlPath;
            controller.HttpContext.Request.QueryString = new QueryString("?id=chk1000");

            URLData urlData = ContextHelper.GetData(controller.HttpContext);

            Assert.Equal(urlData.Directory, urlPath);
            Assert.Equal(urlData.Path, "/some-controller/someaction/chk1000.json");
            Assert.Equal(urlData.FileName, "chk1000.json");
        }
        [Fact]
        public void GetDirectoryAndFilesFromValidURLWithoutQueryString()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<string>())).Returns("expectedData");
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

            URLData urlData = ContextHelper.GetData(controller.HttpContext);

            Assert.Equal("/some-controller/someaction", urlData.Directory);
            Assert.Equal("/some-controller/someaction/chk1000.json", urlData.Path);
            Assert.Equal("chk1000.json", urlData.FileName);
        }

        [Fact]
        public void ThrowExceptionAtBadURL()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<string>())).Throws<FileNotFoundException>();
            HttpContext defaultContext = new DefaultHttpContext();

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            contextAccessorMock.Setup(x => x.HttpContext).Returns(defaultContext);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            StubDataController controller = new StubDataController(dataAccessMock.Object, contextAccessorMock.Object, hostEnvMock.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = defaultContext;

            string urlPath = "";
            controller.HttpContext.Request.Path = urlPath;

            Assert.Throws<BadUrlFormatException>(() => ContextHelper.GetData(controller.HttpContext));
        }

        [Fact]
        public void ThrowExceptionAturlWithNoFile()
        {
            var dataAccessMock = new Mock<IDataAccessService>();
            dataAccessMock.Setup(x => x.ReadFile(It.IsAny<string>())).Throws<FileNotFoundException>();
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

            Assert.Throws<BadUrlFormatException>(() => ContextHelper.GetData(controller.HttpContext));
        }
    }
}
