using System.Data.Entity;

namespace EFScripter
{
    public class DoNothing<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
        }
    }
}