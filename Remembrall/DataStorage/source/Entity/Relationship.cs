namespace DataStorage.source.Entity
{
    public class Relationship
    {
        public int RelationshipId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Relation { get; set; }

    }
}
