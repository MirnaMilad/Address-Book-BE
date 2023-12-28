using Address_Book.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Repository.Data.configurations
{
    internal class EntryConfig : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.HasOne(E => E.Department)
                .WithMany()
                .HasForeignKey(E => E.DepartmentId);

            builder.HasOne(E => E.Job)
                .WithMany()
                .HasForeignKey(E => E.JobId);

            builder.Property(E => E.FullName)
                .IsRequired()
                .HasMaxLength(50);


        }
    }
}
