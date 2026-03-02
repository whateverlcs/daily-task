using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_REWARD, "Create table to save the rewards")]
    public class Version0000005 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Rewards")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Gold").AsString(255).NotNullable();
        }
    }
}