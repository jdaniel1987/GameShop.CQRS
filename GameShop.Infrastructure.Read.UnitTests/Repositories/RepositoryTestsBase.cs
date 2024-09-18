using GameShop.Infrastructure.Read.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Read.UnitTests.Repositories;

public class RepositoryTestsBase<T> : IDisposable
{
    protected ReadOnlyDatabaseContext ReadOnlyDbContext { get; private set; }
    protected T RepositoryUnderTesting { get; private set; }
    protected IFixture Fixture { get; private set; }

    public RepositoryTestsBase()
    {
        var dbName = Guid.NewGuid().ToString();
        var readOnlyDbContextOptions = new DbContextOptionsBuilder<ReadOnlyDatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        ReadOnlyDbContext = new ReadOnlyDatabaseContext(readOnlyDbContextOptions);
        ReadOnlyDbContext.Database.EnsureCreated(); // Ensure database is created

        var readOnlyDbContextFactoryMock = new Mock<IDbContextFactory<ReadOnlyDatabaseContext>>();
        readOnlyDbContextFactoryMock.Setup(factory => factory.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(ReadOnlyDbContext);

        Fixture = new Fixture();
        Fixture.Register(() => ReadOnlyDbContext);

        RepositoryUnderTesting = (T)Activator.CreateInstance(typeof(T), [readOnlyDbContextFactoryMock.Object])!;
    }

    public void Dispose()
    {
        ReadOnlyDbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
