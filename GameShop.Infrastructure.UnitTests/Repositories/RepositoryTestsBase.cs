using GameShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Infrastructure.UnitTests.Repositories;

public class RepositoryTestsBase<T> : IDisposable
{
    protected ReadOnlyDatabaseContext ReadOnlyDbContext { get; private set; }
    protected WriteReadDatabaseContext WriteReadDbContext { get; private set; }
    protected T RepositoryUnderTesting { get; private set; }
    protected IFixture Fixture { get; private set; }

    public RepositoryTestsBase()
    {
        var dbName = Guid.NewGuid().ToString();
        var readOnlyDbContextOptions = new DbContextOptionsBuilder<ReadOnlyDatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var writeReadDbContextOptions = new DbContextOptionsBuilder<WriteReadDatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        ReadOnlyDbContext = new ReadOnlyDatabaseContext(readOnlyDbContextOptions);
        ReadOnlyDbContext.Database.EnsureCreated(); // Ensure database is created
        WriteReadDbContext = new WriteReadDatabaseContext(writeReadDbContextOptions);
        WriteReadDbContext.Database.EnsureCreated(); // Ensure database is created

        var readOnlyDbContextFactoryMock = new Mock<IDbContextFactory<ReadOnlyDatabaseContext>>();
        readOnlyDbContextFactoryMock.Setup(factory => factory.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(ReadOnlyDbContext);
        var writeReadDbContextFactoryMock = new Mock<IDbContextFactory<WriteReadDatabaseContext>>();
        writeReadDbContextFactoryMock.Setup(factory => factory.CreateDbContextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(WriteReadDbContext);

        Fixture = new Fixture();
        Fixture.Register(() => ReadOnlyDbContext);
        Fixture.Register(() => WriteReadDbContext);

        RepositoryUnderTesting = (T)Activator.CreateInstance(typeof(T), [readOnlyDbContextFactoryMock.Object, writeReadDbContextFactoryMock.Object])!;
    }

    public void Dispose()
    {
        ReadOnlyDbContext.Dispose();
        WriteReadDbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
