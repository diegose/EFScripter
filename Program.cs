using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
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
            var configuration = (DbMigrationsConfiguration)Activator.CreateInstance(typeof (DbMigrationsConfiguration<>).MakeGenericType(contextType));
            configuration.AutomaticMigrationsEnabled = true;
            var migrator = new DbMigrator(configuration);
            var scripter = new MigratorScriptingDecorator(migrator);
            Console.WriteLine(scripter.ScriptUpdate(null, null));

        }
    }
}
