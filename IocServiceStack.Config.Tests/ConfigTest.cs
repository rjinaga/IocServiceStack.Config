using Xunit;

namespace IocServiceStack.Config.Tests
{
    public class ConfigTest
    {
        [Fact]
        public void ConfigFromFile_LoadModules_Lenght2()
        {
            //Arrange
            var config = new ContainerOptions();

            //Act
            ConfigExtension.ConfigFromFile(config);

            
            //Assert
            Assert.Equal(2, config.Assemblies.Length);
            Assert.Equal(3, config.Dependencies.Assemblies.Length);
            Assert.Equal(4, config.Dependencies.DependencyOptions.Assemblies.Length);
            Assert.Single(config.SharedDependencies.Assemblies);
        }
    }
}
