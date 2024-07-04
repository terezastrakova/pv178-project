using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class UserVocabularySetConfiguration : IEntityTypeConfiguration<UserVocabularySet>
    {
        public void Configure(EntityTypeBuilder<UserVocabularySet> builder)
        {
            builder.HasKey(uvs => new { uvs.UserId, uvs.VocabularySetId });

            builder.HasOne(uvs => uvs.User)
                .WithMany(u => u.UserVocabularySets)
                .HasForeignKey(uvs => uvs.UserId);

            builder.HasOne(uvs => uvs.VocabularySet)
                .WithMany(vs => vs.UserVocabularySets)
                .HasForeignKey(uvs => uvs.VocabularySetId);
        }
    }
}
