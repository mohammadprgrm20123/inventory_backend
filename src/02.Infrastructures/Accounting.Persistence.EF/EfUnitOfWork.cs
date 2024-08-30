using Framework.Domain;

namespace Accounting.Persistence.EF
{
    public class EfUnitOfWork(ApplicationWriteDbContext context) : UnitOfWork
    {
        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }
    }
}
