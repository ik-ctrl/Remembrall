using System;
using DataStorage.source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.source
{
    public class RemembrallContext : DbContext
    {
        /// <summary>
        /// Счётчик  открытых соединений
        /// </summary>
        private static int _counter = 0;
        public RemembrallContext()
        {

        }

        public RemembrallContext(DbContextOptions<RemembrallContext> options) : base(options)
        {
            _counter++;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _counter++;
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=storageDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationPeopleEntity(modelBuilder);
            ConfigurationEmailEntity(modelBuilder);
            ConfigurationPhoneEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Таблица хранения людей
        /// </summary>
        public DbSet<Person> People { get; set; }
        /// <summary>
        /// Таблица хранения почт
        /// </summary>
        public DbSet<Email> Emails { get; set; }
        /// <summary>
        /// Таблица хранения мобильных телефонов
        /// </summary>
        public DbSet<Phone> Phones { get; set; }
        /// <summary>
        /// Таблица хранения записей
        /// </summary>
        public DbSet<Note> Notes { get; set; }


        #region config region 
        private void ConfigurationPhoneEntity(ModelBuilder builder)
        {
            builder.Entity<Phone>()
                .HasOne(ph => ph.Person)
                .WithMany(people => people.Phones)
                .HasForeignKey(ph => ph.PersonId);
        }

        private void ConfigurationEmailEntity(ModelBuilder builder)
        {
            builder.Entity<Email>()
                .HasOne(em => em.Person)
                .WithMany(people => people.Emails)
                .HasForeignKey(em => em.PersonId);

        }

        private void ConfigurationPeopleEntity(ModelBuilder builder)
        {
            builder.Entity<Person>()
                .Property(pr => pr.Name)
                .IsRequired();

            builder.Entity<Person>()
                .Property(prop => prop.Surname)
                .IsRequired();

            builder.Entity<Person>()
                .Property(p => p.Relation)
                .HasConversion(
                    value=>value.ToString(),
                    value=>(RelationshipEnum)Enum.Parse(typeof(RelationshipEnum),value));
        }
        #endregion



        public void Dispose()
        {
            _counter--;
            base.Dispose();
        }

    }
}
