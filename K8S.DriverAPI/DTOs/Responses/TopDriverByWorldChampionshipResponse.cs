namespace K8S.DriverAPI.DTOs.Responses
{
    public class TopDriverByWorldChampionshipResponse
    {
        public Guid DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
        public int WorldChampionships { get; set; }
    }
}
