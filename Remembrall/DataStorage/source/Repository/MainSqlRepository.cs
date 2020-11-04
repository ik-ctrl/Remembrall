using System;
using DataStorage.Source.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Source.Repository
{
    public class MainSqlRepository:IMainRepository
    {
        private RemembrallContext _context;
        private bool _disposed;

        public MainSqlRepository()
        {
            _context=new RemembrallContext();
            PersonRepository= new SqlPersonRepository(_context);
            NotesRepository=new SqlNoteRepository(_context);
            EmailsRepository= new SqlEmailRepository(_context);
            PhonesRepository = new SqlPhoneRepository(_context);
            SpecialDateRepository = new SqlSpecialDateRepository(_context);
            _disposed = false;
        }


        public IPersonRepository PersonRepository { get; set; }
        public INoteRepository NotesRepository { get; set; }
        public IEmailRepository EmailsRepository { get; set; }
        public IPhoneRepository PhonesRepository { get; set; }
        public ISpecialDateRepository SpecialDateRepository { get; set; }

        public void ResetContext()
        {
            _context = null;
            _context = new RemembrallContext();
            PersonRepository = new SqlPersonRepository(_context);
            NotesRepository = new SqlNoteRepository(_context);
            EmailsRepository = new SqlEmailRepository(_context);
            PhonesRepository = new SqlPhoneRepository(_context);
            SpecialDateRepository= new SqlSpecialDateRepository(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteDatabase()
        {
            _context.DeleteDatabase();
        }

        public bool IsConnect()
        {
            return _context.Database.CanConnect();
        }

        public void DeployMigration()
        {
            _context.Database.Migrate();
        }

        public object Clone()
        {
            return new RemembrallContext();
        }

        #region Dispose
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    PersonRepository = null;
                    NotesRepository = null;
                    EmailsRepository = null;
                    PhonesRepository = null;
                    _context?.Dispose();
                    _disposed = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
