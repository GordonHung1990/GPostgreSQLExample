using AutoMapper;
using GPostgreSQLExample.Repositories.Entities;
using GPostgreSQLExample.Repositories.Models.Players;

namespace GPostgreSQLExample.Repositories.ModelsAutoMapp
{
    public class PlayersAutoMapp : Profile
    {
        public PlayersAutoMapp()
        {
            CreateMap<AddPlayer, Player>()
                .ForMember(target => target.Account, option => option.MapFrom(source => source.Account))
                .ForMember(target => target.Password, option => option.MapFrom(source => source.Password))
                .ForMember(target => target.Status, option => option.MapFrom(source => (short)source.Status));
            CreateMap<AddPlayer, PlayerInfo>();
            CreateMap<UpdatePlayerInfo, PlayerInfo>();
        }
    }
}