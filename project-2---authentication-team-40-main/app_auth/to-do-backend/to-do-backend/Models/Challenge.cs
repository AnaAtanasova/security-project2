using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace to_do_backend.Models
{
    public class Challenge
    {
        public Challenge()
        {
            ChallengeValue = new byte[128 / 8];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(ChallengeValue);

            IterationCount = 0;
            IsFailed = false;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] ChallengeValue { get; private set; }
        public int IterationCount { get; set; }
        public bool IsFailed { get; set; }
        public string secret { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
