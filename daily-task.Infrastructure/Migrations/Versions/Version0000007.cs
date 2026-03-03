using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.REMOVE_STATUS_TABLE_TASK, "Remove Status column from table Task")]
    public class Version0000007 : VersionBase
    {
        public override void Up()
        {
            Delete.Column("Status").FromTable("Tasks");
        }
    }
}
