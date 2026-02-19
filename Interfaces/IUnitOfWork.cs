namespace LifeOs.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}