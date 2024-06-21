namespace BusinessLayer.UnitOfWorks
{
    internal interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
