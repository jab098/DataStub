using System;
using Xunit;
using voa_ps_data_stub.Services;
using voa_ps_data_stub.Services.Interfaces;
using Moq;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace voa_ps_data_stub.Tests
{
    public class DataAccessServiceTests
    {
        [Fact]
        public void ShouldReadFile()
        {
            string expectedData = "{TestData}";
            string fileLocation = "/root/dir/Resources/some/dir/file.json";

            var mock = new Mock<IFileAccess>();
            mock.Setup(x => x.ReadAllText(fileLocation)).Returns(expectedData);
            mock.Setup(x => x.Exists(fileLocation)).Returns(true);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("/root/dir");

            DataAccessService dataAccess = new DataAccessService(mock.Object, hostEnvMock.Object);
            string result = dataAccess.ReadFile("/some/dir/file.json");

            Assert.Equal(expectedData, result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNotPresent()
        {
            string expectedData = "{TestData}";
            bool fileExists = false;

            var mock = new Mock<IFileAccess>();
            mock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns(expectedData);
            mock.Setup(x => x.Exists(It.IsAny<string>())).Returns(fileExists);

            var hostEnvMock = new Mock<IHostingEnvironment>();
            hostEnvMock.Setup(x => x.ContentRootPath).Returns("some/dir");

            DataAccessService dataAccess = new DataAccessService(mock.Object, hostEnvMock.Object);
            Assert.Throws<FileNotFoundException>(() => dataAccess.ReadFile("/some/dir"));
        }
    }
}
