using Newtonsoft.Json;

namespace SpaceTrespassers.Utils
{
    public class ScoreSave
    {
        public ScoreSave()
        {
            PlayerName = "Anonymous";
            ScoredOn = DateTime.Now.ToString("s");
        }

        [JsonProperty]
        public int Points { get; private set; }

        [JsonProperty]
        public string PlayerName { get; private set; }

        [JsonProperty]
        public string ScoredOn { get; private set; }

        public void SaveScore()
            => ScoredOn = DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy");

        public void SetName(string name)
            => PlayerName = name;

        public void AddPoints(int points)
            => Points += points;

        public string ScoreBoardInfo()
            => $"{PlayerName} scored {Points} points! ({ScoredOn})";

        public override string ToString()
        {
            return $"Score: {Points}";
        }
    }
}
