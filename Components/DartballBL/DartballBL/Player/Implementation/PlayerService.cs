using Dartball.BusinessLayer.Player.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Shared;
using System.Linq;

namespace Dartball.BusinessLayer.Player.Implementation
{
    public class PlayerService : IPlayerService
    {
        private IMapper Mapper;

        public PlayerService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Player, PlayerDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public IPlayer GetPlayer(Guid playerId)
        {
            IPlayer player = null;

            using (var context = new Data.DartballContext())
            {
                var item = context.Players.FirstOrDefault(x => x.PlayerId == playerId.ToString());
                if (item != null) player = Mapper.Map<PlayerDto>(item);
            }

            return player;
        }

        public List<IPlayer> GetPlayers()
        {
            List<IPlayer> players = new List<IPlayer>();
            using (var context = new Data.DartballContext())
            {
                var items = context.Players.Where(x => !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) players.Add(Mapper.Map<PlayerDto>(item));
            }
            return players;
        }

        public List<IPlayer> GetPlayers(List<Guid> playerIds) {
            List<IPlayer> players = new List<IPlayer>();

            List<string> playerIdsAsStrings = new List<string>();
            foreach (Guid id in playerIds) playerIdsAsStrings.Add(id.ToString());

            using(var context = new Data.DartballContext()) {
                var items = context.Players.Where(x => playerIdsAsStrings.Contains(x.PlayerId) && !x.DeleteDate.HasValue).ToList();
                foreach (var item in items) Mapper.Map<PlayerDto>(item);
            }

            return players;
        }

        public ChangeResult Save(IPlayer player)  {
            bool isAdd = false;

            if (player.PlayerId == Guid.Empty) isAdd = true;
            else if (GetPlayer(player.PlayerId) == null) isAdd = true;

            if (isAdd) return AddNew(player);
            else return Update(player);
        }



        public ChangeResult AddNew(IPlayer player)
        {
            return AddNew(new List<IPlayer> { player });
        }
        public ChangeResult AddNew(List<IPlayer> players)
        {
            var result = Validate(players);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var player in players)
                    {
                        context.Players.Add(new Domain.Player()
                        {
                            PlayerId = player.PlayerId == Guid.Empty ? Guid.NewGuid().ToString() : player.PlayerId.ToString(),
                            Name = Helper.CleanString(player.Name),
                            LastName = Helper.CleanString(player.LastName),
                            EmailAddress = Helper.CleanString(player.EmailAddress),
                            UserName = Helper.CleanString(player.UserName),
                            Password = Helper.CleanString(player.Password), //TODO: add encryption
                            ShouldSync = player.ShouldSync ? 1 : 0,
                            DeleteDate = player.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        public ChangeResult Update(IPlayer player)
        {
            return Update(new List<IPlayer> { player });
        }
        public ChangeResult Update(List<IPlayer> players)
        {
            var result = Validate(players);
            if (result.IsSuccess)
            {
                using (var context = new Data.DartballContext())
                {
                    foreach (var player in players)
                    {
                        context.Players.Update(new Domain.Player()
                        {
                            PlayerId = player.PlayerId.ToString(),
                            Name = Helper.CleanString(player.Name),
                            LastName = Helper.CleanString(player.LastName),
                            EmailAddress = Helper.CleanString(player.EmailAddress),
                            UserName = Helper.CleanString(player.UserName),
                            Password = Helper.CleanString(player.Password), //TODO: add encryption
                            ShouldSync = player.ShouldSync ? 1 : 0,
                            DeleteDate = player.DeleteDate
                        });
                    }
                    context.SaveChanges(); 
                }
            }
            return result;
        }



        private ChangeResult Validate(List<IPlayer> players)
        {
            ChangeResult result = new ChangeResult();

            foreach(var player in players)
            {
                if (!result.IsSuccess) break;
                if (string.IsNullOrWhiteSpace(player.Name))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Player Fist Name Required.");
                }
                else if (player.Name.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Player First Name cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(player.LastName) && player.LastName.Trim().Length > 100) {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Player Last Name cannot be longer than 100 characters.");
                }

                if (string.IsNullOrWhiteSpace(player.UserName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("User Name is Required.");
                }
                else if (player.UserName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("User Name cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(player.EmailAddress))
                {
                    if (!Helper.IsValidEmail(player.EmailAddress))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid Email Address: {player.EmailAddress}");
                    }
                }
            }

            return result;
        }




        public ChangeResult Remove(Guid playerId)
        {
            ChangeResult result = new ChangeResult();

            using (var context = new Data.DartballContext())
            {
                var item = context.Players.FirstOrDefault(x => x.PlayerId == playerId.ToString());
                if (item != null)
                {
                    context.Players.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }

    }
}
