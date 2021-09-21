﻿using Microsoft.EntityFrameworkCore;
using MySqlProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlProject.Data.Migrations
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)

        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(@"server=localhost;port=3306;database=db;uid=root;");
            }
        }
       
        //public DbSet<Product> Products { get; set; }
    }
}
