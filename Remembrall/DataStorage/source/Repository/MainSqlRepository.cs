using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using DataStorage.Source.Entity;

namespace DataStorage.Source.Repository
{
    class MainSqlRepository:IMainRepository
    {
        private RemembrallContext _context;
        private bool disposed;

        public MainSqlRepository()
        {
            _context=new RemembrallContext();
            PersonRepository= new SqlPersonRepository(_context);
            NotesRepository=new SqlNoteRepository(_context);
            EmailsRepository= new SqlEmailRepository(_context);
            PhonesRepository = new SqlPhoneRepository(_context);
            disposed = false;
        }

        public IRepository<Person> PersonRepository { get; set; }
        public IRepository<Note> NotesRepository { get; set; }
        public IRepository<Email> EmailsRepository { get; set; }
        public IRepository<Phone> PhonesRepository { get; set; }
        public void ResetContext()
        {
            _context = null;
            _context = new RemembrallContext();
            PersonRepository = new SqlPersonRepository(_context);
            NotesRepository = new SqlNoteRepository(_context);
            EmailsRepository = new SqlEmailRepository(_context);
            PhonesRepository = new SqlPhoneRepository(_context);
        }
        public object Clone()
        {
            return new RemembrallContext();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    PersonRepository = null;
                    NotesRepository = null;
                    EmailsRepository = null;
                    PhonesRepository = null;
                    _context?.Dispose();
                    disposed = true;
                }
            }
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
