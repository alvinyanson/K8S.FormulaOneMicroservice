namespace K8S.Contracts
{
    public class AchievementCreated
    {
        public int RaceWins { get; set; }
        public int PolePosition { get; set; }
        public int FastestLap { get; set; }
        public int WorldChampionship { get; set; }
        public Guid DriverId { get; set; }

        public override string ToString()
        {
            return $"Achievement Created:\n" +
                   $"- Driver ID: {DriverId}\n" +
                   $"- World Championships: {WorldChampionship}\n" +
                   $"- Pole Positions: {PolePosition}\n" +
                   $"- Fastest Laps: {FastestLap}\n" +
                   $"- Race Wins: {RaceWins}";
        }
    }
}
