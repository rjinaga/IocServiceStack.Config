namespace IocServiceStack.Config
{
    public class AppDependenciesObject
    {
        public ModuleDependencies AppDependencies { get; set; }
        public ModuleDependencies SharedDependencies { get; set; }
        public bool StrictMode { get; set; } = true;
        public AdditionInfo[] Addition { get; set; }
    }
}
