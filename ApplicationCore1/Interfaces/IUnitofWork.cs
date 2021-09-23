using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUnitofWork
    {
        public IGenericRepository<Adjective> Adjective { get; }
        public IGenericRepository<Client> Client { get; }
        public IGenericRepository<ClientResponses> ClientResponses { get; }
        public IGenericRepository<Friend> Friend { get; }
        public IGenericRepository<FriendResponses> FriendResponses { get; }

        int Commit();

        Task<int> CommitAsync();
    }
}
