using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameInningTeamBatterService : IGameInningTeamBatterService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameInningTeamBatterRepository Repository;

        public GameInningTeamBatterService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameInningTeamBatterDto, GameInningTeamBatterDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameInningTeamBatterRepository();
        }



        public IGameInningTeamBatter GetGameInningTeamBatter(Guid gameInningTeamAlternateKey, int sequence)
        {
            IGameInningTeamBatter gameInningTeamBatter = null;

            var dl = Repository.LoadByCompositeKey(gameInningTeamAlternateKey, sequence);
            if (dl != null) gameInningTeamBatter = Mapper.Map<GameInningTeamBatterDto>(dl);

            return gameInningTeamBatter;
        }

        public List<IGameInningTeamBatter> GetGameInningTeamBatters(Guid gameInningTeamAlternateKey)
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>();

            foreach (var item in Repository.LoadByGameInningTeamAlternateKey(gameInningTeamAlternateKey).OrderBy(x => x.Sequence))
            {
                if (!item.DeleteDate.HasValue) gameInningTeamBatters.Add(Mapper.Map<GameInningTeamBatterDto>(item));
            }

            return gameInningTeamBatters;
        }


        public List<IGameInningTeamBatter> GetGamePlayerAtBats(Guid gameAlternateKey, Guid playerAlteranteKey)
        {
            List<IGameInningTeamBatter> gameInningTeamBatters = new List<IGameInningTeamBatter>();


            return gameInningTeamBatters;
        }


    }
}
