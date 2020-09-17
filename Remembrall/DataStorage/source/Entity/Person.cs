using System.Collections.Generic;

namespace DataStorage.Source.Entity
{
    public class Person
    {
        public Person()
        {
            Phones=new List<Phone>();
            Emails = new List<Email>();
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public RelationshipEnum Relation { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Email> Emails { get; set; }

    }
}
