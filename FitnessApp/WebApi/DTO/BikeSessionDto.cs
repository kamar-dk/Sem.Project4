namespace WebApi.DTO
{
    public class BikeSessionDto
    {
        public int BikeSessionId { get; set; }
        public DateTime Date { get; set; }
        public float Durration { get; set; }
        public float Distance { get; set; }
        public float AvgSpeed { get; set; }
        public string? Note { get; set; }

    }
}
