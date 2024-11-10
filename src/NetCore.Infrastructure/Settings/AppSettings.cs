namespace NetCore.Infrastructure.Settings
{
    public class AppSettings
    {
        public Redis Redis { get; set; }
        public Telemetry Telemetry { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public PgSql PgSql { get; set; }
        public MsSql MsSql { get; set; }
        public Cache Cache { get; set; }
    }
}
