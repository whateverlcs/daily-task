using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.ADD_USER_TABLE_PROFILE, "Add User column to Profile table")]
    public class Version0000006 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("Profile")
                .AddColumn("User").AsString(255).NotNullable().WithDefaultValue("John Doe");
        }
    }
}
