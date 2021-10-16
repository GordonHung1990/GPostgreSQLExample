using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Models.Players;

namespace GPostgreSQLExample.Repositories
{
    public interface IPlayersRepository
    {
        ValueTask AddAsync(AddPlayer source);

        ValueTask UpdateInfoAsync(UpdatePlayerInfo source);

        ValueTask<GetPlayer> GetAsync(Guid playerId);

        ValueTask<GetPlayer> GetAsync(string account);
    }
}