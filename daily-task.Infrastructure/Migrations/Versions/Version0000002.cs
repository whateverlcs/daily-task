using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.ADD_GOLD_TABLE_TASK, "Add Gold column to Tasks table")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Tasks")
                .AddColumn("Gold").AsString(255).NotNullable().WithDefaultValue("1g");
        }
    }
}