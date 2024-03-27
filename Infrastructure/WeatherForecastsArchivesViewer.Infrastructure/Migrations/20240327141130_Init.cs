using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecastsArchivesViewer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weather_forecasts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    temperature = table.Column<float>(type: "real", nullable: false),
                    air_humidity = table.Column<float>(type: "real", nullable: false),
                    dew_point = table.Column<float>(type: "real", nullable: false),
                    atmospheric_pressure = table.Column<int>(type: "integer", nullable: false),
                    wind_direction = table.Column<string>(type: "text", nullable: true),
                    wind_speed = table.Column<int>(type: "integer", nullable: true),
                    cloud_cover = table.Column<int>(type: "integer", nullable: true),
                    lower_limit_cloud_cover = table.Column<int>(type: "integer", nullable: true),
                    horizontal_visibility = table.Column<int>(type: "integer", nullable: true),
                    weather_event = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather_forecasts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weather_forecasts");
        }
    }
}
