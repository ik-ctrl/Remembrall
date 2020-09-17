namespace DataStorage.Source.Entity
{
    public class Relationship
    {
        public int RelationshipId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public RelationshipEnum Relation { get; set; }

    }
}
