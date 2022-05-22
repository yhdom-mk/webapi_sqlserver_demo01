using System.Text.Json.Serialization;

namespace TuneWebApp01.Models
{
    public class Tune
    //internal class Tune
    {
        public int TuneId { get; set; }
        //[JsonPropertyName("TuneName")]
        public string? TuneName { get; set; }
        public string? Album { get; set; }
        public string? DateOfJoining { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
