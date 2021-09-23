﻿using ApplicationCore.Interfaces;
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

        private IGenericRepository<Category> _Category;
        private IGenericRepository<FoodType> _FoodType;
        private IGenericRepository<MenuItem> _MenutItem;
        private IGenericRepository<ApplicationUser> _ApplicationUser;

        public IGenericRepository<ApplicationUser> ApplicationUser
        {
            get
            {
                if (_ApplicationUser == null) _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
                return _ApplicationUser;
            }
        }

        public IGenericRepository<Category> Category
        {
            get
            {
                if (_Category == null) _Category = new GenericRepository<Category>(_dbContext);
                return _Category;
            }  
        }

        public IGenericRepository<FoodType> FoodType
        {
            get
            {
                if (_FoodType == null) _FoodType = new GenericRepository<FoodType>(_dbContext);
                return _FoodType;
            }
        }

        public IGenericRepository<MenuItem> MenuItem
        {
            get
            {
                if (_MenutItem == null) _MenutItem = new GenericRepository<MenuItem>(_dbContext);
                return _MenutItem;
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
