using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class Article :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        
        //Forgein keys
    }

    public class ArticlesFluentContext
    {
        public void InitContext(ModelBuilder builder)
        {
            // Primary key
            builder.Entity<Article>()
                .HasKey(u => u.Id );  
            
            // Data
        }
    }
}