using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upload_File_MVC_Core.Models
{
    public class FilesContext : DbContext
    {
        public DbSet<File> File { get; set; }
        public DbSet<Persons> Persons { get; set; }

        public FilesContext(DbContextOptions<FilesContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}