using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Models.Players;

namespace GPostgreSQLExample.Services
{
    public interface IPlayersService
    {
        ValueTask<GetPlayer> GetAsync(Guid playerId);

        ValueTask<GetPlayer> GetAsync(string account);

        ValueTask<bool> AddAsync(AddPlayer source);

        ValueTask<bool> UpdateInfoAsync(UpdatePlayerInfo source);
    }
}