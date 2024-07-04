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
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.EnglishWord).IsRequired();
            builder.Property(w => w.JapaneseWord).IsRequired();

            builder.HasOne(w => w.VocabularySet)
                .WithMany(vs => vs.Words)
                .HasForeignKey(w => w.VocabularySetId);
        }
    }
}
