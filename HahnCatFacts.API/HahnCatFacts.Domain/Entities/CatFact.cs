namespace HahnCatFacts.Domain.Entities
{
    public class CatFact
    {
        public long Id { get; set; }
        public required string Description { get; set; }
    }
}
