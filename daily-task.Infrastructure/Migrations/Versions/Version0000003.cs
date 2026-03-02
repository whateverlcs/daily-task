using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace daily_task.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_RANK, "Create table to save the ranks created")]
    public class Version0000003 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Ranks")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("TaskGoal").AsInt32().NotNullable();
        }
    }
}
