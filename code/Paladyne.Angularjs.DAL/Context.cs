using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.DAL
{
    public class Context : IdentityDbContext<User>, IUnitOfWork
    {
        public Context()
            : this("name=Paladyne.Angularjs.DAL.Context")
        { }
        public Context(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModule>()
                .HasOptional(x => x.Granter)
                .WithMany(x => x.Granted);

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<UserModule> UserModules { get; set; }
    }
}
