using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using voa_ps_data_stub.Helpers;

namespace voa_ps_data_stub.Tests.Helpers
{
    public class URLDataTests
    {
        [Fact]
        public void ShouldReturnCorrectPath()
        {
            string directory = "/some/dir/";
            string fileName = "newFile.json";

            URLData urlData = new URLData(directory, fileName);

            Assert.Equal("/some/dir/newFile.json", urlData.Path);
        }
        [Fact]
        public void ShouldReturnPathWithAdditionalSlash()
        {
            string directory = "/some/dir";
            string fileName = "newFile.json";

            URLData urlData = new URLData(directory, fileName);

            Assert.Equal("/some/dir/newFile.json", urlData.Path);
        }
        [Fact]
        public void FileNameShouldRemoveLeadingSlash()
        {
            string directory = "/some/dir";
            string fileName = "/newFile.json";

            URLData urlData = new URLData(directory, fileName);

            Assert.Equal("newFile.json", urlData.FileName);
        }
    }
}
