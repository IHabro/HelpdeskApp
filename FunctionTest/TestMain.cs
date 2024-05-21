using DataLayer.DbContexts;
using FunctionTest.DatabaseTables;
using Microsoft.EntityFrameworkCore;

namespace FunctionTest
{
    internal class TestMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----Testing application for HelpDesk Web Application-----");
            var dbContext = GetIdContext();

            CoreTables.FillCoreTables(dbContext);
            CoreTables.FillIncidents(dbContext);

            JoinTables.FillLevelToAction(dbContext);
            JoinTables.FillUserToAction(dbContext);
            JoinTables.FillRoleToProjects(dbContext);
            JoinTables.FillUserToProjectToRole(dbContext);
            JoinTables.FillActionToProjectToRole(dbContext);

            Console.WriteLine("-----Testing application for HelpDesk Web Application-----");
        }

        static IdentityDbContext GetIdContext()
        {
            const string connectionString = "Server=.;Database=HelpdeskApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";

            var IdentityOptionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            IdentityOptionsBuilder.UseSqlServer(connectionString);

            return new IdentityDbContext(IdentityOptionsBuilder.Options);
        }
    }
}
