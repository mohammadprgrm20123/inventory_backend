namespace Framework.Domain.Data
{
    public interface UnitOfWork
    {
        Task Complete();
    }
}
