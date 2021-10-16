using System;

namespace GPostgreSQLExample.Repositories.Models.Players
{
    public record UpdatePlayerInfo
    {
        public Guid PlayerId { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string PhoneNumber { get; set; }

        public string Mailbox { get; set; }
    }
}