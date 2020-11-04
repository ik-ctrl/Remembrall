using System;
using System.Collections.Generic;
using DataStorage.Source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source
{
    public class RemembrallContext : DbContext
    {

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
            //todo: вынести  в файл настройки  строку подключения
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=storageDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationPeopleEntity(modelBuilder);
            ConfigurationEmailEntity(modelBuilder);
            ConfigurationPhoneEntity(modelBuilder);
            ConfigurationSpecialDatesEntity(modelBuilder);
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

        /// <summary>
        /// Таблица для хранения специальных дат
        /// </summary>
        public DbSet<SpecialDate> SpecialDates { get; set; }

        /// <summary>
        /// Счётчик открытых соединений
        /// </summary>
        public int Counter
        {
            get => _counter;
        }

        #region config region 

        /// <summary>
        /// Конфигурирование таблицы телефонов
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigurationPhoneEntity(ModelBuilder builder)
        {
            builder.Entity<Phone>()
                .HasOne(ph => ph.Person)
                .WithMany(people => people.Phones)
                .HasForeignKey(ph => ph.PersonId);
        }

        /// <summary>
        /// Конфигурирование таблицы телефонов
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigurationEmailEntity(ModelBuilder builder)
        {
            builder.Entity<Email>()
                .HasOne(em => em.Person)
                .WithMany(people => people.Emails)
                .HasForeignKey(em => em.PersonId);

        }

        /// <summary>
        /// Конфигурирование таблицы телефонов
        /// </summary>
        /// <param name="builder"></param>
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
                    value => value.ToString(),
                    value => (RelationshipEnum)Enum.Parse(typeof(RelationshipEnum), value));

        }

        /// <summary>
        /// Конфигурирование таблицы телефонов
        /// </summary>
        /// <param name="builder"></param>
        ///
        private void ConfigurationSpecialDatesEntity(ModelBuilder builder)
        {
            var dates = new List<SpecialDate>()
            {
                new SpecialDate() {SpecialDateId = 1, Day = 1, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 2, Day = 2, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 3, Day = 3, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 4, Day = 4, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 5, Day = 5, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 6, Day = 6, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 7, Day = 7, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 8, Day = 8, Month = 1, Description = "Новогодние выходные"},
                new SpecialDate() {SpecialDateId = 9, Day = 14, Month = 2, Description = "День всех влюбленных"},
                new SpecialDate() {SpecialDateId = 10, Day = 23, Month = 2, Description = "День защитника отечества"},
                new SpecialDate() {SpecialDateId = 11, Day = 8, Month = 3, Description = "праздник женщин"},
                new SpecialDate() {SpecialDateId = 12, Day = 1, Month = 5, Description = "Мир, труд, май!!!"},
                new SpecialDate() {SpecialDateId = 13, Day = 3, Month = 5, Description = "Мир, труд, май!!!"},
                new SpecialDate() {SpecialDateId = 14, Day = 9, Month = 5, Description = "День Победы"},
                new SpecialDate() {SpecialDateId = 15, Day = 12, Month = 6, Description = "День независимости"},
                new SpecialDate() {SpecialDateId = 16, Day = 1, Month = 9, Description = "День знаний"},
                new SpecialDate() {SpecialDateId = 17, Day = 4, Month = 11, Description = "День народного единства"},
                new SpecialDate() {SpecialDateId = 18, Day = 31, Month = 12, Description = "Новый год! С праздником!!!"},
            };
            builder.Entity<SpecialDate>().HasData(dates);
        }
        #endregion

        public void DeleteDatabase()
        {
            Database.EnsureDeleted();
        }
        public void Dispose()
        {
            _counter--;
            base.Dispose();
        }

    }
}
