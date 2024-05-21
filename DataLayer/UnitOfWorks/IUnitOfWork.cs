namespace DataLayer.UnitOfWorks
{
    internal interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
