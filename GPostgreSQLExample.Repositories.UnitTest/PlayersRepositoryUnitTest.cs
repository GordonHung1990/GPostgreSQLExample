using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Models.Players;
using NSubstitute;
using NUnit.Framework;

namespace GPostgreSQLExample.Repositories.UnitTest
{
    [TestFixture]
    public class PlayersRepositoryUnitTest
    {
        [Test]
        public async Task AddAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            _ = fakeRepository.AddAsync(Arg.Any<AddPlayer>());
            await fakeRepository.AddAsync(Arg.Any<AddPlayer>()).ConfigureAwait(false);
        }

        [Test]
        public async Task UpdateInfoAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            _ = fakeRepository.UpdateInfoAsync(Arg.Any<UpdatePlayerInfo>());
            await fakeRepository.UpdateInfoAsync(Arg.Any<UpdatePlayerInfo>()).ConfigureAwait(false);
        }

        [Test]
        public async Task GetAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            _ = fakeRepository.GetAsync(Arg.Any<Guid>()).Returns(ValueTask.FromResult<GetPlayer>(null));
            var result = await fakeRepository.GetAsync(Arg.Any<Guid>()).ConfigureAwait(false);
            Assert.AreEqual(null, result);
        }

        [Test]
        public async Task GetAccountAsync()
        {
            var fakeRepository = Substitute.For<IPlayersRepository>();
            _ = fakeRepository.GetAsync(Arg.Any<string>()).Returns(ValueTask.FromResult<GetPlayer>(null));
            var result = await fakeRepository.GetAsync(Arg.Any<string>()).ConfigureAwait(false);
            Assert.AreEqual(null, result);
        }
    }
}