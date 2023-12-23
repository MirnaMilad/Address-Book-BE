using Address_Book.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Core.services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser User);
    }
}
