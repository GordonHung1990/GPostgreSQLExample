namespace GPostgreSQLExample.Repositories.Models.Players
{
    public record AddPlayer
    {
        public string Account { get; set; } = default!;

        public string Password { get; set; } = default!;

        public PlayerStatus Status { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string PhoneNumber { get; set; }

        public string Mailbox { get; set; }
    }
}