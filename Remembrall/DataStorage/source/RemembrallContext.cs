﻿using System;
using System.Collections.Generic;
using System.Text;
using DataStorage.source.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.source
{
    public class RemembrallContext:DbContext 
    {
        public RemembrallContext()
        {
        
        }

        public RemembrallContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=storageDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person>People { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Relationship> Relations { get; set; }




    }
}
