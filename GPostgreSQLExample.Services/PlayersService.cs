using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories;
using GPostgreSQLExample.Repositories.Models.Players;
using Microsoft.Extensions.Logging;

namespace GPostgreSQLExample.Services
{
    internal class PlayersService : IPlayersService
    {
        private readonly ILogger<PlayersService> _logger;
        private readonly IPlayersRepository _repository;

        public PlayersService(
            ILogger<PlayersService> logger,
            IPlayersRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ValueTask<GetPlayer> GetAsync(Guid playerId)
            => _repository.GetAsync(playerId);

        public ValueTask<GetPlayer> GetAsync(string account)
           => _repository.GetAsync(account);

        public async ValueTask<bool> AddAsync(AddPlayer source)
        {
            var player = await _repository.GetAsync(source.Account).ConfigureAwait(false);
            if (player != null)
            {
                return false;
            }
            await _repository.AddAsync(source).ConfigureAwait(false);
            return true;
        }

        public async ValueTask<bool> UpdateInfoAsync(UpdatePlayerInfo source)
        {
            var player = await _repository.GetAsync(source.PlayerId).ConfigureAwait(false);
            if (player == null)
            {
                return false;
            }
            await _repository.UpdateInfoAsync(source).ConfigureAwait(false);
            return true;
        }
    }
}