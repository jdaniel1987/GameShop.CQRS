using GameShop.Infrastructure.Write.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.Write.UnitTests.Repositories;

public class RepositoryTestsBase<T> : IDisposable
{
    protected WriteReadDatabaseContext WriteReadDbContext { get; private set; }
    protected T RepositoryUnderTesting { get; private set; }
    protected IFixture Fixture { get; private set; }

    public RepositoryTestsBase()
    {
        var dbName = Guid.NewGuid().ToString();
        var writeReadDbContextOptions = new DbContextOptionsBuilder<WriteReadDatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        WriteReadDbContext = new WriteReadDatabaseContext(writeReadDbContextOptions);
        WriteReadDbContext.Database.EnsureCreated(); // Ensure database is created

        var writeReadDbContextFactoryMock = new Mock<IDbContextFactory<WriteReadDatabaseContext>>();
        writeReadDbContextFactoryMock.Setup(factory => factory.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(WriteReadDbContext);

        Fixture = new Fixture();
        Fixture.Register(() => WriteReadDbContext);

        RepositoryUnderTesting = (T)Activator.CreateInstance(typeof(T), [writeReadDbContextFactoryMock.Object])!;
    }

    public void Dispose()
    {
        WriteReadDbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
