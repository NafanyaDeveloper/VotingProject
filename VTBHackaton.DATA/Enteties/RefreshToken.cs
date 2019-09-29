using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class RefreshToken
    {
        public string Token { get; set; }

        public Guid UserId { get; set; }

        public DateTime ExpiresDate { get; set; }
    }
}
