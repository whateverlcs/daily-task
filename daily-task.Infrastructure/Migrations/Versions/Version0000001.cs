using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_TASK, "Create table to save the tasks created")]
    public class Version0000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Tasks")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Priority").AsInt32().NotNullable()
                .WithColumn("Description").AsString(2000).NotNullable()
                .WithColumn("Status").AsInt32().NotNullable();
        }
    }
}