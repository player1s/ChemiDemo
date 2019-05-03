namespace ChemiDemo.Web.Infrastructure
{
    using System;
    using System.Reflection;
    using ChemiDemo.DataContext.Repositories;
    using FluentMigrator;
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;
    using FluentMigrator.Runner.Processors.SQLite;

    public class Migrator
    {
        private readonly string connection;

        private Migrator(string connection, Action<IMigrationRunner> runner, Type type)
        {
            this.connection = connection;
            
            runner(GetRunner(Assembly.GetAssembly(type)));
        }

        public static void Migrate(Action<IMigrationRunner> runner) => new Migrator("Data Source=data.sqlite;Version=3;New=True;", runner, typeof(NHibernateRepository));

        private MigrationRunner GetRunner(Assembly assembly)
        {
            var factory = new SQLiteProcessorFactory();
            var announcer = new NullAnnouncer();

            var processor = factory.Create(connection, announcer, new MigrationOptions() { PreviewOnly = false, Timeout = 0 });

            return new MigrationRunner(assembly, new RunnerContext(announcer), processor);
        }

        private class MigrationOptions
            : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }

            public int Timeout { get; set; }

            public string ProviderSwitches { get; set; }
        }
    }
}