namespace IocServiceStack.Config.Tests
{
    using System;
    
    public class Customer : ICustomer
    {
        public void Create()
        {
            Console.WriteLine("Customer Created");
        }
    }
}
