using System;
using System.Collections.Generic;
using System.Text;

namespace Unjumble.Core.Models.Dtos
{
    public class UserCompaniesDto
    {
        public bool IsOwner { get; set; }

        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string City { get; set; }
    }
}
