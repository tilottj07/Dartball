using AutoMapper;
using DartballBL.League.Dto;
using DartballBL.League.Interface.Models;
using DartballBL.Shared;
using DartballBL.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartballBL.League.Implementation
{
    public class LeagueService : ILeagueService
    {
        private IMapper Mapper;
        private DataLayer.Device.Interface.ILeague LeagueRepository;

        public LeagueService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Device.LeagueRepository.League, LeagueDto>());
            Mapper = mapConfig.CreateMapper();

            LeagueRepository = new DataLayer.Device.LeagueRepository();
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
                    DataLayer.Device.LeagueRepository.League item = new DataLayer.Device.LeagueRepository.League()
                    {
                        Name = Helper.CleanString(league.Name),
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




    }
}
