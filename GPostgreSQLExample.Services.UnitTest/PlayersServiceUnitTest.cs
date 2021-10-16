using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories;
using GPostgreSQLExample.Repositories.Models.Players;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NUnit.Framework;

namespace GPostgreSQLExample.Services.UnitTest
{
    [TestFixture]
    public class PlayersServiceUnitTest
    {
        [Test]
        public async Task GetAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            var faceLog = NullLogger<PlayersService>.Instance;
            var service = new PlayersService(faceLog, fakeRepository);
            var data = Guid.NewGuid();
            var result = await service.GetAsync(data).ConfigureAwait(false);
            _ = await fakeRepository.Received(1)
                  .GetAsync(data);
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task GetAccountAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            var faceLog = NullLogger<PlayersService>.Instance;
            var service = new PlayersService(faceLog, fakeRepository);
            var data = "test";
            var result = await service.GetAsync(data).ConfigureAwait(false);
            _ = await fakeRepository.Received(1)
                  .GetAsync(data);
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task AddAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            var faceLog = NullLogger<PlayersService>.Instance;
            var service = new PlayersService(faceLog, fakeRepository);
            var data = new AddPlayer();
            var result = await service.AddAsync(data).ConfigureAwait(false);
            await fakeRepository.Received(1)
                .GetAsync(data.Account);
            await fakeRepository.Received(1)
                 .AddAsync(data);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task UpdateInfoAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            var faceLog = NullLogger<PlayersService>.Instance;
            var service = new PlayersService(faceLog, fakeRepository);
            var data = new UpdatePlayerInfo()
            {
                PlayerId = Guid.NewGuid(),
                LastName = "test",
                FullName = "test",
                NickName = "test",
                PhoneNumber = "123456789",
                Mailbox = "test@test.com"
            };
            _ = fakeRepository.GetAsync(data.PlayerId).Returns(ValueTask.FromResult<GetPlayer>(new()
            {
                PlayerId = Guid.NewGuid(),
                LastName = "test",
                FullName = "test",
                NickName = "test",
                PhoneNumber = "123456789",
                Mailbox = "test@test.com"
            }));
            var result = await service.UpdateInfoAsync(data).ConfigureAwait(false);

            await fakeRepository.Received(1)
                       .GetAsync(data.PlayerId);
            await fakeRepository.Received(1)
                         .UpdateInfoAsync(data);
            Assert.AreEqual(true, result);
        }
    }
}