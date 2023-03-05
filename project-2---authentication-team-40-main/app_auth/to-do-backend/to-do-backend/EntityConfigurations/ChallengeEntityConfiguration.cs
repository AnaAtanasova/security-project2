using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using to_do_backend.Models;

namespace to_do_backend.EntityConfigurations
{
    public class ChallengeEntityConfiguration : IEntityTypeConfiguration<Challenge>
    {
        private List<Challenge> challenges;

        public ChallengeEntityConfiguration()
        {
            this.challenges = new List<Challenge>();
        }
        public ChallengeEntityConfiguration(List<Challenge> challenges)
        {
            this.challenges = challenges;
        }

        public void Configure(EntityTypeBuilder<Challenge> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.ChallengeValue);
            builder.Property(c => c.IsFailed);
            builder.Property(c => c.IterationCount);
            builder.Property(c => c.secret);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Challenges)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(challenges);
        }
    }
}
