using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;
using LanguageHelper.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageHelper.DataAccessLayer
{

    public class UnitOfWork : IUnitOfWork
    {
        protected readonly LanguageHelperDbContext Context;
        private RoleRepository _roleRepository;
        private SheetRepository _sheetRepository;
        private UserRepository _userRepository;
        private UserLanguageRepository _userLanguageRepository;

        public UnitOfWork(LanguageHelperDbContext context) => this.Context = context;

        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(Context));

        public ISheetRepository SheetRepository => _sheetRepository ?? (_sheetRepository = new SheetRepository(Context));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(Context));

        public IUserLanguageRepository UserLanguageRepository => _userLanguageRepository ?? (_userLanguageRepository = new UserLanguageRepository(Context));

        public int SaveChages() => Context.SaveChanges();

        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = Context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);
                if (changes == 0) return true;
                return await Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

    }

}
