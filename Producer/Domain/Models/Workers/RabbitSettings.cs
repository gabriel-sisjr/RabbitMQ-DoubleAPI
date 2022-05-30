namespace Domain.Models.Workers
{
    public class RabbitSettings
    {
        public string HostName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ushort Port { get; set; }
    }
}
