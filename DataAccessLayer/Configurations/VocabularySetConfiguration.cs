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
    public class VocabularySetConfiguration : IEntityTypeConfiguration<VocabularySet>
    {
        public void Configure(EntityTypeBuilder<VocabularySet> builder)
        {
            builder.HasKey(vs => vs.Id);
            builder.Property(vs => vs.Name).IsRequired();
        }
    }
}
