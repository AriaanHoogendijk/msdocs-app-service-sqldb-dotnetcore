using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Models;

namespace DotNetCoreSqlDb.Data
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext (DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FieldLabel> FieldLabels { get; set; }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; } = default!;
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AreaView> AreaViews { get; set; }
        public virtual DbSet<TypeOfAreaView> TypeOfAreaViews { get; set; }
        public virtual DbSet<TypeOfArea> TypeOfAreas { get; set; }

    }
}
