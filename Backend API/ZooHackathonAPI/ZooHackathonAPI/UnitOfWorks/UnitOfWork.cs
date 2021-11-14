using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooHackathonAPI.UnitOfWorks
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
