using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Amigos.DataAccessLayer
{
    public class AmigoDBContext : DbContext
    {
        public DbSet<Amigos.Models.Amigo> Amigos { get; set; }
    }
}