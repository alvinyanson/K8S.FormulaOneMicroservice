using System.Text.Json.Serialization;

namespace K8S.DriverStatAPI.DTOs.Responses
{
    public class DriverAchievementResponse
    {
        [JsonPropertyName("driverId")]
        public Guid DriverId { get; set; }
        [JsonPropertyName("worldChampionship")]
        public int WorldChampionship { get; set; }
        [JsonPropertyName("polePosition")]
        public int PolePosition { get; set; }
        [JsonPropertyName("fastestLap")]
        public int FastestLap { get; set; }
        [JsonPropertyName("wins")]
        public int Wins { get; set; }
    }
}
