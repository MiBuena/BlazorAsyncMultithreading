using Moq;
using NUnit.Framework;
using PopulationCensus.Server.Interfaces;
using PopulationCensus.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PopulationCensus.UnitTests.LocalFileServiceTests
{
    public class ReadFileAsStreamAsyncTests
    {
        [Test]
        public void Should_ThrowAnException_When_TokenIsCancelled()
        {
            //Arrange
            var fakeFileContents = "First line\nSecond line";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<string>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            CancellationTokenSource a = new CancellationTokenSource();
            a.Cancel();


            //Assert
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                var collections = localFileService.ReadFileAsStream(a.Token);

                await foreach (var collection in collections)
                {
                }
            });
        }

        [Test]
        public void Should_ReadCorrectLines_When_TokenIsNotCancelled()
        {
            //Arrange
            var fakeFileContents = "First line\nSecond line";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<string>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            CancellationTokenSource a = new CancellationTokenSource();

            //Act
            var lines = localFileService.ReadFileAsStream(a.Token);

            //Assert
            Assert.AreEqual(lines.FirstAsync().Result, "Second line");
        }
    }
}
