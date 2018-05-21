using Dartball.BusinessLayer.Player.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dartball.BusinessLayer.Player.Dto;
using Dartball.BusinessLayer.Player.Interface.Models;
using Dartball.BusinessLayer.Shared.Models;
using Dartball.BusinessLayer.Shared;

namespace Dartball.BusinessLayer.Player.Implementation
{
    public class PlayerService : IPlayerService
    {
        private IMapper Mapper;
        private DataLayer.Device.Repository.PlayerRepository PlayerRepository;

        public PlayerService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<DataLayer.Device.Dto.PlayerDto, PlayerDto>());
            Mapper = mapConfig.CreateMapper();

            PlayerRepository = new DataLayer.Device.Repository.PlayerRepository();
        }


        public IPlayer GetPlayer(Guid playerAlternateKey)
        {
            IPlayer player = null;

            var dl = PlayerRepository.LoadByKey(playerAlternateKey);
            if (dl != null) player = Mapper.Map<PlayerDto>(dl);

            return player;
        }

        public List<IPlayer> GetPlayers()
        {
            List<IPlayer> players = new List<IPlayer>();
            foreach(var item in PlayerRepository.LoadAll())
            {
                if (!item.DeleteDate.HasValue) players.Add(Mapper.Map<PlayerDto>(item));
            }
            return players;
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
                foreach(var player in players)
                {
                    DataLayer.Device.Dto.PlayerDto dto = new DataLayer.Device.Dto.PlayerDto()
                    {
                        PlayerAlternateKey = player.PlayerAlternateKey == Guid.Empty ? Guid.NewGuid().ToString() : player.PlayerAlternateKey.ToString(),
                        Name = Helper.CleanString(player.Name),
                        Photo = player.Photo,
                        EmailAddress = Helper.CleanString(player.EmailAddress),
                        UserName = Helper.CleanString(player.UserName),
                        Password = Helper.CleanString(player.Password), //TODO: add encryption
                        ShouldSync = player.ShouldSync ? 1 : 0,
                        DeleteDate = player.DeleteDate
                    };
                    PlayerRepository.AddNew(dto);
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
                foreach (var player in players)
                {
                    DataLayer.Device.Dto.PlayerDto dto = new DataLayer.Device.Dto.PlayerDto()
                    {
                        PlayerAlternateKey = player.PlayerAlternateKey.ToString(),
                        Name = Helper.CleanString(player.Name),
                        Photo = player.Photo,
                        EmailAddress = Helper.CleanString(player.EmailAddress),
                        UserName = Helper.CleanString(player.UserName),
                        Password = Helper.CleanString(player.Password), //TODO: add encryption
                        ShouldSync = player.ShouldSync ? 1 : 0,
                        DeleteDate = player.DeleteDate
                    };
                    PlayerRepository.Update(dto);
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
                    result.ErrorMessages.Add("Player Name Required.");
                }
                else if (player.Name.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Player Name cannot be longer than 100 characters.");
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




        public ChangeResult Remove(Guid playerAlternateKey)
        {
            ChangeResult result = new ChangeResult();
            PlayerRepository.Delete(playerAlternateKey);

            return result;
        }

    }
}
