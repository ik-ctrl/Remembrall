namespace DataStorage.Source.Entity
{
    public class Email
    {
        public int EmailId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Mail { get; set; }

    }
}
