using HRMS.Core.Entities.Auth;
using HRMS.Core.Entities.BaseEntities;
using HRMS.Core.Entities.EntityLogs;
using HRMS.Infrastructure.Extensions;
using HRMS.Infrastructure.Helper.Acls;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace HRMS.Infrastructure.DatabaseContext;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ISignInHelper signInHelper,
    ILoggerFactory loggerFactory) : IdentityDbContext<
    IdentityModel.User,
    IdentityModel.Role,
    long,
    IdentityModel.UserClaim,
    IdentityModel.UserRole,
    IdentityModel.UserLogin,
    IdentityModel.RoleClaim,
    IdentityModel.UserToken>(options)
{
    private readonly ISignInHelper _signInHelper = signInHelper;
    private readonly ILoggerFactory _loggerFactory = loggerFactory;

    public DbSet<AuditLog> AuditLogs { get; set; }

    #region Model Configuration
    /// <summary>
    /// Configures the model for the context by applying entity configurations and custom conventions.
    /// </summary>
    /// <remarks>This method applies all configurations from the current assembly and enforces custom
    /// conventions for relationships, date and time properties, decimal properties, and table naming. Override this
    /// method to customize model configuration for the context.</remarks>
    /// <param name="modelBuilder">The builder used to construct the model for the context. Provides configuration options for entity types,
    /// relationships, and conventions.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations automatically
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Custom conventions
        modelBuilder.RelationConvetion();
        modelBuilder.DateTimeConvention();
        modelBuilder.DecimalConvention();
        modelBuilder.ConfigureDecimalProperties();
        modelBuilder.PluralzseTableNameConventions();
    }
    #endregion

    #region DbContext Configuration
    /// <summary>
    /// Configures the database context options for this instance, including logging and warning behaviors.
    /// </summary>
    /// <remarks>This method sets up logging to output SQL queries and informational messages, and
    /// configures the context to ignore pending model changes warnings. Override this method to customize context
    /// configuration, but ensure to call the base implementation to preserve default behaviors.</remarks>
    /// <param name="optionsBuilder">The builder used to configure options for the database context, such as logging, warnings, and database
    /// providers. Must not be null.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // Ignore pending model changes warnings
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        // Logging
        optionsBuilder
            .UseLoggerFactory(_loggerFactory) // Proper DI logger factory
            .LogTo(Console.WriteLine, LogLevel.Information)
            .LogTo(query => WriteSqlQueryLog(query), LogLevel.Debug);
    }
    #endregion

    #region SaveChanges Overrides
    /// <summary>
    /// Saves all changes made in this context to the underlying database.
    /// </summary>
    /// <remarks>Before saving, this method tracks and updates auditable entities to ensure audit
    /// information is persisted. If an error occurs during the save operation, no changes are written to the
    /// database. This method overrides the base implementation to provide additional auditing
    /// functionality.</remarks>
    /// <returns>The number of state entries written to the database. This value may be zero if no changes were detected.</returns>
    public override int SaveChanges()
    {
        TrackAuditableEntities();
        return base.SaveChanges();
    }
    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <remarks>This method tracks auditable entities before saving changes. If an error occurs
    /// during the save operation, the exception is logged to the console and then rethrown. The returned value
    /// indicates how many entities were affected by the save operation.</remarks>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous save operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            TrackAuditableEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SaveChangesAsync Error] {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }

    private void TrackAuditableEntities()
    {
        Audit();
        AuditTrail();
    }
    #endregion

    #region Auditing
    /// <summary>
    /// Updates auditing properties for tracked entities that implement the AuditableEntity type, setting creation
    /// and modification metadata as appropriate before changes are saved.
    /// </summary>
    /// <remarks>This method sets the CreatedBy and CreatedDate properties for newly added entities,
    /// and the ModifiedBy and ModifiedDate properties for modified entities, using the current authenticated user
    /// and the current UTC time. It should be called prior to persisting changes to ensure audit fields are
    /// correctly populated. This method does not persist changes itself.</remarks>
    private void Audit()
    {
        long userId = _signInHelper.IsAuthenticated ? (long)_signInHelper.UserId : 0;
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = entry.Entity.CreatedBy != 0 ? entry.Entity.CreatedBy : userId;
                entry.Entity.CreatedDate = entry.Entity.CreatedDate == DateTimeOffset.MinValue ? now : entry.Entity.CreatedDate;
            }
            else
            {
                entry.Entity.ModifiedBy ??= userId;
                entry.Entity.ModifiedDate ??= now;
            }
        }
    }
    /// <summary>
    /// Records audit information for entity changes detected in the current context, creating audit log entries for
    /// added, modified, or deleted entities.
    /// </summary>
    /// <remarks>This method tracks changes to entities, excluding those derived from BaseEntity or
    /// AuditLog, and generates audit logs reflecting the type of change, affected properties, and the user
    /// responsible. Audit entries are added to the AuditLogs collection for persistence. This method should be
    /// called after changes are detected to ensure accurate audit logging. Thread safety is not guaranteed; ensure
    /// appropriate synchronization if used in multi-threaded scenarios.</remarks>
    private void AuditTrail()
    {
        long userId = _signInHelper.IsAuthenticated ? (long)_signInHelper.UserId : 0;

        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => !(e.Entity is BaseEntity || e.Entity is AuditLog) &&
                                 e.State != EntityState.Detached &&
                                 e.State != EntityState.Unchanged))
        {
            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                UserId = userId
            };
            auditEntries.Add(auditEntry);

            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;

                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.AuditType = AuditType.Update;
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                }
            }
        }

        foreach (var auditEntry in auditEntries)
        {
            AuditLogs.Add(auditEntry.ToAuditLog());
        }
    }
    #endregion
    /// <summary>
    /// Writes the specified SQL query to the designated log destination based on the provided store type.
    /// </summary>
    /// <remarks>Use <see cref="StoreType.Output"/> to write the query to the debug output. Other
    /// store types may require additional configuration or implementation to enable logging to a database or
    /// file.</remarks>
    /// <param name="query">The SQL query string to be logged. Cannot be null.</param>
    /// <param name="storeType">Specifies the destination for logging the query. Defaults to <see cref="StoreType.Output"/> if not provided.</param>
    #region SQL Logging
    private static void WriteSqlQueryLog(string query, StoreType storeType = StoreType.Output)
    {
        switch (storeType)
        {
            case StoreType.Output:
                Debug.WriteLine(query);
                break;
            case StoreType.Db:
                // Implement DB storage if needed
                break;
            case StoreType.File:
                // Example: store in file
                // File.AppendAllText("sql_log.txt", query + Environment.NewLine);
                break;
        }
    }
    #endregion
}

public enum StoreType
{
    Db,
    File,
    Output
}
