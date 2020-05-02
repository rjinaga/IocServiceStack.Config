namespace IocServiceStack.Config
{
    public class ModuleDependencies
    {
        public string Name { get; set; }
        public string[] Modules { get; set; }
        public ModuleDependencies Dependencies { get; set; }
    }
}
