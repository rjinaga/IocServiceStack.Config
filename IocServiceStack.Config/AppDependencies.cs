namespace IocServiceStack.Config
{
    internal class AppDependencies
    {
        public string Name { get; set; }
        public string[] Modules { get; set; }
        public AppDependencies Dependencies { get; set; }
    }
}
