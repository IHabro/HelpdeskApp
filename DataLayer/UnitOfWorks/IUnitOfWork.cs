namespace DataLayer.UnitOfWorks
{
    internal interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
