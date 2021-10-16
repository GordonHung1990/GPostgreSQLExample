using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GPostgreSQLExample.Repositories.Entities;
using GPostgreSQLExample.Repositories.Models.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GPostgreSQLExample.Repositories
{
    internal class PlayersRepository : IPlayersRepository
    {
        private readonly ILogger<PlayersRepository> _logger;
        private readonly MainContext _mainContext;
        private readonly IMapper _mapper;

        public PlayersRepository(
            ILogger<PlayersRepository> logger,
            MainContext mainContext,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mainContext = mainContext ?? throw new ArgumentNullException(nameof(mainContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async ValueTask AddAsync(AddPlayer source)
        {
            var player = _mapper.Map<Player>(source);
            var playerInfo = _mapper.Map<PlayerInfo>(source);
            var playerId = Guid.NewGuid();
            var systTime = DateTimeOffset.UtcNow;
            player.CreateTime = systTime.DateTime;
            player.ModifyTime = systTime.DateTime;
            _ = await _mainContext.Players.AddAsync(player).ConfigureAwait(false);
            _ = await _mainContext.PlayerInfos.AddAsync(playerInfo).ConfigureAwait(false);
            _ = await _mainContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async ValueTask UpdateInfoAsync(UpdatePlayerInfo source)
        {
            var playerInfo = _mapper.Map<PlayerInfo>(source);
            _ = _mainContext.PlayerInfos.Update(playerInfo);
            _ = await _mainContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async ValueTask<GetPlayer> GetAsync(Guid playerId)
        => await (from player in _mainContext.Players
                  join playerInfo in _mainContext.PlayerInfos
                  on player.PlayerId equals playerInfo.PlayerId
                  where player.PlayerId == playerId
                  select new GetPlayer
                  {
                      PlayerId = player.PlayerId,
                      Account = player.Account,
                      Status = (PlayerStatus)player.Status,
                      LastName = playerInfo.LastName,
                      FullName = playerInfo.FullName,
                      NickName = playerInfo.NickName,
                      PhoneNumber = playerInfo.PhoneNumber,
                      Mailbox = playerInfo.Mailbox
                  }).FirstOrDefaultAsync().ConfigureAwait(false);

        public async ValueTask<GetPlayer> GetAsync(string account)
        => await (from player in _mainContext.Players
                  join playerInfo in _mainContext.PlayerInfos
                  on player.PlayerId equals playerInfo.PlayerId
                  where player.Account == account
                  select new GetPlayer
                  {
                      PlayerId = player.PlayerId,
                      Account = player.Account,
                      Status = (PlayerStatus)player.Status,
                      LastName = playerInfo.LastName,
                      FullName = playerInfo.FullName,
                      NickName = playerInfo.NickName,
                      PhoneNumber = playerInfo.PhoneNumber,
                      Mailbox = playerInfo.Mailbox
                  }).FirstOrDefaultAsync().ConfigureAwait(false);
    }
}