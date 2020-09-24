using System.Collections.Generic;
using System.Linq;
using DataStorage.Source.Entity;
using DataStorage.Source.Repository;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {

        [Test]
        public void MainRepositoryTest()
        {

            var main = new MainSqlRepository();
            
            var person1=new Person(){Name="kiril",Surname = "ignat"};
            main.PersonRepository.Add(person1);
            main.SaveChanges();

            var email1 = new Email() { Mail = "sssssss",Person = person1};
            var email2 = new Email() { Mail = "aaaaa" ,Person = person1};

            main.EmailsRepository.AddRange(new List<Email>(){ email1, email2 });
            
            main.SaveChanges();

            var emails = main.EmailsRepository.Find(item => item.Mail.Contains("s")).ToList();
            Assert.AreEqual(1,emails.Count);

            var item1 = main.EmailsRepository.GetItem(0);
            Assert.AreEqual("sssssss",item1);

            var item2 = main.EmailsRepository.GetAllItem();
            Assert.AreEqual(2,item2.Count());

            main.DeleteDatabase();

        }
    
    }
}