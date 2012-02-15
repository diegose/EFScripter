using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace EFScripter
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.DefaultConnectionFactory = new SqlConnectionFactory(args[0]);
            var assembly = Assembly.LoadFrom(args[1]);
            var contextType = assembly.GetExportedTypes().Single(x => typeof (DbContext).IsAssignableFrom(x));
            var dbContext = (IObjectContextAdapter)Activator.CreateInstance(contextType);
            dynamic initializer = Activator.CreateInstance(typeof (DoNothing<>).MakeGenericType(contextType));
            Database.SetInitializer(initializer);
            Console.WriteLine(dbContext.ObjectContext.CreateDatabaseScript());
        }
    }
}
