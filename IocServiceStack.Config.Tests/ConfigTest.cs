using System;
using Xunit;

namespace IocServiceStack.Config.Tests
{
    public class ConfigTest
    {
        [Fact]
        public void ConfigFromFile_NoException()
        {
            //Arrange
            var config = new ContainerConfig();

            //Act
            var exception = Record.Exception(() => ConfigExtension.ConfigFromFile(config));

            //Assert
            Assert.Null(exception);
        }
    }
}
