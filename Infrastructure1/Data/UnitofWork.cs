using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitofWork : IUnitofWork
    {
        //dependancy injection
        private readonly ApplicationDbContext _dbContext;

        public UnitofWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Adjective> _Adjective;
        private IGenericRepository<Client> _Client;
        private IGenericRepository<ClientResponses> _ClientResponses;
        private IGenericRepository<Friend> _Friend;
        private IGenericRepository<FriendResponses> _FriendResponses;
        private IGenericRepository<ApplicationUser> _ApplicationUser;

        public IGenericRepository<Adjective> Adjective
        {
            get
            {
                if (_Adjective == null) _Adjective = new GenericRepository<Adjective>(_dbContext);
                return _Adjective;
            }
        }

        public IGenericRepository<Client> Client
        {
            get
            {
                if (_Client == null) _Client = new GenericRepository<Client>(_dbContext);
                return _Client;
            }  
        }

        public IGenericRepository<ClientResponses> ClientResponses
        {
            get
            {
                if (_ClientResponses == null) _ClientResponses = new GenericRepository<ClientResponses>(_dbContext);
                return _ClientResponses;
            }
        }

        public IGenericRepository<Friend> Friend
        {
            get
            {
                if (_Friend == null) _Friend = new GenericRepository<Friend>(_dbContext);
                return _Friend;
            }
        }
        
        public IGenericRepository<FriendResponses> FriendResponses
        {
            get
            {
                if (_FriendResponses == null) _FriendResponses = new GenericRepository<FriendResponses>(_dbContext);
                return _FriendResponses;
            }
        }

        public IGenericRepository<ApplicationUser> ApplicationUser
        {
            get
            {
                if (_ApplicationUser == null) _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
                return _ApplicationUser;
            }
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
