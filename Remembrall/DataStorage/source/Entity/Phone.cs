namespace DataStorage.Source.Entity
{
    public class Phone
    {
        public int PhoneId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
