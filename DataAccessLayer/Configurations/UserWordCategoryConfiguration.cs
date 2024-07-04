using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class UserWordCategoryConfiguration : IEntityTypeConfiguration<UserWordCategory>
    {
        public void Configure(EntityTypeBuilder<UserWordCategory> builder)
        {
            builder.HasKey(uwc => new { uwc.WordId, uwc.CategoryId, uwc.UserId });
            
            builder.HasOne(uwc => uwc.Word)
                .WithMany(w => w.UserWordCategories)
                .HasForeignKey(uwc => uwc.WordId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(uwc => uwc.Category)
                .WithMany(c => c.UserWordCategories)
                .HasForeignKey(uwc => uwc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(uwc => uwc.User)
                .WithMany(u => u.UserWordCategories)
                .HasForeignKey(uwc => uwc.UserId)
                .OnDelete(DeleteBehavior.Cascade); // users do not get deleted so this is unnecessary, but if option to delete users is added in the future ...
        }
    }
}
