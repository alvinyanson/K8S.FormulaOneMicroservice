namespace K8S.DriverAPI.DTOs.Responses
{
    public class TopDriver
    {
        public Guid DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
    }
}
