using Dartball.BusinessLayer.Game.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;

namespace Dartball.BusinessLayer.Game.Implementation
{
    public class GameInningTeamBatterService : IGameInningTeamBatterService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.GameInningTeamBatterRepository Repository;
        private IGameEventService EventService;

        public GameInningTeamBatterService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<DataLayer.Device.Dto.GameInningTeamBatterDto, GameInningTeamBatterDto>());
            Mapper = mapConfig.CreateMapper();

            Repository = new DataLayer.Device.Repository.GameInningTeamBatterRepository();
            EventService = new GameEventService();
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

            foreach(var item in Repository.LoadByGameAlternateKeyPlayerAlternateKey(gameAlternateKey, playerAlteranteKey))
            {
                if (!item.DeleteDate.HasValue) gameInningTeamBatters.Add(Mapper.Map<GameInningTeamBatterDto>(item));
            }

            return gameInningTeamBatters;
        }


        public ChangeResult AddNew(IGameInningTeamBatter gameInningTeamBatter)
        {
            return AddNew(new List<IGameInningTeamBatter> { gameInningTeamBatter });
        }
        public ChangeResult AddNew(List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            var result = Validate(gameInningTeamBatters, isAddNew: true);
            if (result.IsSuccess)
            {
                foreach(var item in gameInningTeamBatters)
                {
                    DataLayer.Device.Dto.GameInningTeamBatterDto dto = new DataLayer.Device.Dto.GameInningTeamBatterDto()
                    {
                        GameInningTeamBatterAlternateKey = item.GameInningTeamBatterAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningTeamBatterAlternateKey.ToString(),
                        GameInningTeamAlternateKey = item.GameInningTeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        Sequence = item.Sequence,
                        EventType = item.EventType,
                        TargetEventType = item.TargetEventType,
                        RBIs = item.RBIs,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.AddNew(dto);
                }
            }
            return result;
        }

        public ChangeResult Update(IGameInningTeamBatter gameInningTeamBatter)
        {
            return Update(new List<IGameInningTeamBatter> { gameInningTeamBatter });
        }
        public ChangeResult Update(List<IGameInningTeamBatter> gameInningTeamBatters)
        {
            var result = Validate(gameInningTeamBatters, isAddNew: false);
            if (result.IsSuccess)
            {
                foreach (var item in gameInningTeamBatters)
                {
                    DataLayer.Device.Dto.GameInningTeamBatterDto dto = new DataLayer.Device.Dto.GameInningTeamBatterDto()
                    {
                        GameInningTeamBatterAlternateKey = item.GameInningTeamBatterAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : item.GameInningTeamBatterAlternateKey.ToString(),
                        GameInningTeamAlternateKey = item.GameInningTeamAlternateKey.ToString(),
                        PlayerAlternateKey = item.PlayerAlternateKey.ToString(),
                        Sequence = item.Sequence,
                        EventType = item.EventType,
                        TargetEventType = item.TargetEventType,
                        RBIs = item.RBIs,
                        DeleteDate = item.DeleteDate
                    };
                    Repository.Update(dto);
                }
            }
            return result;
        }



        private ChangeResult Validate(List<IGameInningTeamBatter> gameInningTeamBatters, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            List<int> validEvents = new List<int>();
            foreach (var item in EventService.GetAllGameEvents()) validEvents.Add((int)item.Event);

            foreach (var item in gameInningTeamBatters)
            {
                if (!result.IsSuccess) break;

                if (item.GameInningTeamAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Game Inning Team Key");
                }

                if (item.PlayerAlternateKey == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Player");
                }

                if (item.Sequence < 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Sequence must be > 0");
                }

                if (item.RBIs < 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Cannot have RBIs < 0");
                }

                if (!validEvents.Contains(item.EventType))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid Event Type: {item.EventType}");
                }

                if (item.TargetEventType.HasValue)
                {
                    if (!validEvents.Contains(item.TargetEventType.Value))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid Target Event Type: {item.TargetEventType.Value}");
                    }
                }

                if (isAddNew == false)
                {
                    if (item.GameInningTeamBatterAlternateKey == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid PK");
                    }
                }
            }

            return result;
        }



        public ChangeResult Remove(Guid gameInningTeamAlternateKey, int sequence)
        {
            ChangeResult result = new ChangeResult();
            Repository.Delete(gameInningTeamAlternateKey, sequence);
            return result;
        }

    }
}
