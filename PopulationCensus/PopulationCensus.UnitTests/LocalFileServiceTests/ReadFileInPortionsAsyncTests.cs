using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PopulationCensus.Server.Interfaces;
using PopulationCensus.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.UnitTests.LocalFileServiceTests
{
    public class ReadFileInPortionsAsyncTests
    {
        [Test]
        public async Task Should_ReturnFirstLineAfterTitle_When_ContentWithTwoLines()
        {
            string fakeFileContents = "Code,Description,SortOrder\r\n888,Median age,2";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();

            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);
            var portions = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            await foreach (var portion in portions)
            {
                var firstLine = portion.FirstOrDefault();
                Assert.AreEqual("888,Median age,2", firstLine);
            }
        }
    }
}
