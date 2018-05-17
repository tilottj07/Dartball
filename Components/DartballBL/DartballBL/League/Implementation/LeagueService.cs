using AutoMapper;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.League.Interface.Models;
using Dartball.DataLayer.Device.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.League.Dto;
using Dartball.BusinessLayer.Shared;
using Dartball.BusinessLayer.Shared.Models;

namespace Dartball.BusinessLayer.League.Implementation
{
    public class LeagueService : ILeagueService
    {
        private IMapper Mapper;
        private LeagueRepository LeagueRepository;

        public LeagueService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Device.Dto.LeagueDto, LeagueDto>());
            Mapper = mapConfig.CreateMapper();

            LeagueRepository = new LeagueRepository();
        }



        public ILeague GetLeague(string name)
        {
            ILeague league = null;

            var dl = LeagueRepository.LoadByName(name);
            if (dl != null && !dl.DeleteDate.HasValue) league = Mapper.Map<LeagueDto>(dl);

            return league;
        }

        public List<ILeague> GetLeagues()
        {
            List<ILeague> leagues = new List<ILeague>();

            foreach (var dl in LeagueRepository.LoadAll())
            {
                if (!dl.DeleteDate.HasValue) leagues.Add(Mapper.Map<LeagueDto>(dl));
            }

            return leagues;
        }



        public ChangeResult Save(ILeague league)
        {
            return Save(new List<ILeague> { league });
        }
        public ChangeResult Save(List<ILeague> leagues)
        {
            var result = Validate(leagues);
            if (result.IsSuccess)
            {
                foreach (var league in leagues)
                {
                    DataLayer.Device.Dto.LeagueDto item = new DataLayer.Device.Dto.LeagueDto()
                    {
                        Name = Helper.CleanString(league.Name),
                        Password = Helper.CleanString(league.Password), //TODO: add encryption
                        DeleteDate = null
                    };
                    LeagueRepository.Save(item);
                }
            }
            return result;
        }


        private ChangeResult Validate(List<ILeague> leagues)
        {
            ChangeResult result = new ChangeResult();

            foreach (var league in leagues)
            {
                if (!result.IsSuccess) break;

                if (string.IsNullOrWhiteSpace(league.Name))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("League Name is required");
                }
                else if (league.Name.Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Leage Name must be less than 100 characters");
                }
            }

            return result;
        }



        public ChangeResult RemoveLeague(string name)
        {
            ChangeResult result = new ChangeResult();
            LeagueRepository.Delete(name);

            return result;
        }


    }
}
