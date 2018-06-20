using AutoMapper;
using Dartball.BusinessLayer.League.Interface;
using Dartball.BusinessLayer.League.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dartball.BusinessLayer.League.Dto;
using Dartball.BusinessLayer.Shared;
using Dartball.BusinessLayer.Shared.Models;
using System.Linq;

namespace Dartball.BusinessLayer.League.Implementation
{
    public class LeagueService : ILeagueService
    {
        private IMapper Mapper;

        public LeagueService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.League, LeagueDto>());
            Mapper = mapConfig.CreateMapper();
        }



        public ILeague GetLeague(Guid leagueId)
        {
            ILeague league = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.Leagues.FirstOrDefault(x => x.LeagueId == leagueId.ToString());
                if (item != null) league = Mapper.Map<LeagueDto>(item);
            }

            return league;
        }

        public List<ILeague> GetLeagues()
        {
            List<ILeague> leagues = new List<ILeague>();

            using (var context = new Data.DartballContext())
            {
                var items = context.Leagues.ToList().Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) leagues.Add(Mapper.Map<LeagueDto>(item));
            }

            return leagues;
        }


        public ChangeResult AddNew(ILeague league)
        {
            return AddNew(new List<ILeague> { league });
        }
        public ChangeResult AddNew(List<ILeague> leagues)
        {
            var result = Validate(leagues);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var league in leagues)
                    {
                        context.Leagues.Add(new Domain.League()
                        {
                            Name = Helper.CleanString(league.Name),
                            LeagueId = league.LeagueId == Guid.Empty ? Guid.NewGuid().ToString() : league.LeagueId.ToString(),
                            Password = Helper.CleanString(league.Password), //TODO: add encryption
                            DeleteDate = null
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }



        public ChangeResult Update(ILeague league)
        {
            return Update(new List<ILeague> { league });
        }
        public ChangeResult Update(List<ILeague> leagues)
        {
            var result = Validate(leagues);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var league in leagues)
                    {
                        context.Leagues.Update(new Domain.League()
                        {
                            Name = Helper.CleanString(league.Name),
                            LeagueId = league.LeagueId.ToString(),
                            Password = Helper.CleanString(league.Password), //TODO: add encryption
                            DeleteDate = null
                        });
                    }
                    context.SaveChanges();
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
                    result.ErrorMessages.Add("League Name must be less than 100 characters");
                }
            }

            return result;
        }



        public ChangeResult RemoveLeague(Guid leagueAltenateKey)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.DartballContext())
            {
                var item = context.Leagues.FirstOrDefault(x => x.LeagueId == leagueAltenateKey.ToString());
                if (item != null)
                {
                    context.Leagues.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }


    }
}
