using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalApiTemplate.Infrastructure.Persistence.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class CreateEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "template");

            migrationBuilder.Sql(
                @"
                CREATE TABLE [template].[Event]
                (
                    [EventId] BIGINT IDENTITY(1,1) NOT NULL,
                    [InsertedDate] DATETIME NOT NULL DEFAULT(GETUTCDATE()),
                    [LastUpdatedDate] DATETIME NULL,
                    [JsonData] NVARCHAR(MAX) NOT NULL,
                    [EventType] NVARCHAR(100) NOT NULL,
                    CONSTRAINT PK_Event PRIMARY KEY (EventId)
                )
                GO
            "
            );

            migrationBuilder.Sql(
                @"
                CREATE VIEW [template].[v_Event] WITH SCHEMABINDING
                AS
                SELECT EventId, 
                    InsertedDate,
                    CAST(JSON_VALUE(JsonData, '$.EventType') AS NVARCHAR(255)) AS [EventType],
                    CAST(JSON_VALUE(JsonData, '$.ReferenceId') AS NVARCHAR(255)) AS [ReferenceId],
                    JSON_VALUE(JsonData, '$.Target.Type') As [TargetType],
                    COALESCE(JSON_VALUE(JsonData, '$.Target.Old'), JSON_QUERY(JsonData, '$.Target.Old')) AS [TargetOld],
                    COALESCE(JSON_VALUE(JsonData, '$.Target.New'), JSON_QUERY(JsonData, '$.Target.New')) AS [TargetNew],
                    JSON_QUERY(JsonData, '$.Comments') AS [Comments],
                    [JsonData]
                FROM [template].[Event]
            "
            );

            migrationBuilder.Sql("CREATE UNIQUE CLUSTERED INDEX PK_V_EVENT ON [template].[v_Event] (EventId)");

            migrationBuilder.Sql("CREATE INDEX IX_V_EVENT_EventType_ReferenceId ON [template].[v_Event] (EventType, ReferenceId)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE [template].[Event]");
            migrationBuilder.Sql("DROP View [template].[v_Event]");
        }
    }
}
