using FluentMigrator;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_PROFILE, "Create table to save the data from the user")]
    public class Version0000004 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Profile")
                .WithColumn("TasksCreated").AsInt32().NotNullable()
                .WithColumn("TasksCompleted").AsInt32().NotNullable()
                .WithColumn("GoldEarned").AsInt64().NotNullable()
                .WithColumn("GoldSpent").AsInt64().NotNullable()
                .WithColumn("GoldBalance").AsInt64().NotNullable()
                .WithColumn("ClaimedRewards").AsInt32().NotNullable()
                .WithColumn("RankId").AsInt64().NotNullable().ForeignKey("FK_Profile_Rank_Id", "Ranks", "Id");
        }
    }
}