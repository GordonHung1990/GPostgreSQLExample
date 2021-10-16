using System;
using System.Threading.Tasks;
using GPostgreSQLExample.Repositories.Models.Players;
using GPostgreSQLExample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GPostgreSQLExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(ILogger<PlayersController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ValueTask<GetPlayer> Get(
            [FromServices] IPlayersService service,
            Guid id)
            => service.GetAsync(id);

        /// <summary>
        /// Posts the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="source">The source.</param>
        /// <remarks>
        /// POST
        ///     {
        ///         "account": "gordon",
        ///         "password": "123456",
        ///         "status": 0,
        ///         "lastName": "Hung",
        ///         "fullName": "Gordon",
        ///         "nickName": "Gordon",
        ///         "phoneNumber": "",
        ///         "mailbox": ""
        ///     }
        /// </remarks>
        [HttpPost]
        public ValueTask<bool> Post(
                [FromServices] IPlayersService service,
                [FromBody] AddPlayer source)
            => service.AddAsync(source);

        /// <summary>
        /// Puts the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="source">The source.</param>
        [HttpPut("{id}")]
        public ValueTask<bool> Put(
            [FromServices] IPlayersService service,
            Guid id,
            [FromBody] UpdatePlayerInfo source)
         => service.UpdateInfoAsync(source);
    }
}