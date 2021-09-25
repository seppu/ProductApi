using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Data.Migrations
{
    public static class MigrationBuilderExtensions
    {
        public static void TurnOnTemporalTableSupport(this MigrationBuilder builder,
                string tableName, string historyTableSchema)
        {
            builder.Sql($@"ALTER TABLE {tableName} ADD 
                    SysStartTime datetime2(0) GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
                    SysEndTime datetime2(0) GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
                    PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime);");

            builder.Sql($@"ALTER TABLE {tableName} 
                    SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = {historyTableSchema}.{tableName} ));");
        }

        public static void TurnOffTemporalTableSupport(this MigrationBuilder builder, string tableName)
        {
            builder.Sql($@"ALTER TABLE {tableName} 
                    SET (SYSTEM_VERSIONING = OFF);");
        }
    }
}
