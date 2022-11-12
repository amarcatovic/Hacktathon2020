using System;

namespace Unjumble.Core.Models
{
    public class ActivationCode
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public DateTime ExpiryDate { get; set; }

        public ActivationCode()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 4);
            ExpiryDate = DateTime.Now.AddDays(1);
        }
    }
}
