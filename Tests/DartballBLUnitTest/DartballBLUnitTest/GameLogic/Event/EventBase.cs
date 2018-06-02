using Dartball.BusinessLayer.Game.Dto;
using Dartball.BusinessLayer.Game.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Dartball.BusinessLayer.Game.Implementation.GameEventService;

namespace DartballBLUnitTest.GameLogic.Event
{
    public class EventBase
    {
        public EventBase()
        {
            TEST_GAME_INNING_TEAM_ALTERNATE_KEY = Guid.NewGuid();

            TEST_PLAYER_ONE = Guid.NewGuid();
            TEST_PLAYER_TWO = Guid.NewGuid();
            TEST_PLAYER_THREE = Guid.NewGuid();
            TEST_PLAYER_FOUR = Guid.NewGuid();
            TEST_PLAYER_FIVE = Guid.NewGuid();
        }

        private Guid TEST_GAME_INNING_TEAM_ALTERNATE_KEY;
        private int TEST_SEQUENCE_TRACKER;
        private Guid TEST_PLAYER_ONE;
        private Guid TEST_PLAYER_TWO;
        private Guid TEST_PLAYER_THREE;
        private Guid TEST_PLAYER_FOUR;
        private Guid TEST_PLAYER_FIVE;


        public IGameInningTeamBatter GetTestOutAtBat()
        {
            TEST_SEQUENCE_TRACKER++;
            return new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE_TRACKER,
                PlayerAlternateKey = GetPlayerAlternateKey(),
                EventType = (int)EventType.Out,
                RBIs = 0,
                TargetEventType = (int)EventType.Single
            };
        }

        public IGameInningTeamBatter GetTestSingleAtBat(int rBIs = 0)
        {
            TEST_SEQUENCE_TRACKER++;
            return new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE_TRACKER,
                PlayerAlternateKey = GetPlayerAlternateKey(),
                EventType = (int)EventType.Single,
                RBIs = rBIs,
                TargetEventType = (int)EventType.Single
            };
        }

        public IGameInningTeamBatter GetTestDoubleAtBat(int rBIs = 0)
        {
            TEST_SEQUENCE_TRACKER++;
            return new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE_TRACKER,
                PlayerAlternateKey = GetPlayerAlternateKey(),
                EventType = (int)EventType.Double,
                RBIs = rBIs,
                TargetEventType = (int)EventType.Double
            };
        }

        public IGameInningTeamBatter GetTestTripleAtBat(int rBIs = 0)
        {
            TEST_SEQUENCE_TRACKER++;
            return new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE_TRACKER,
                PlayerAlternateKey = GetPlayerAlternateKey(),
                EventType = (int)EventType.Triple,
                RBIs = rBIs,
                TargetEventType = (int)EventType.Triple
            };
        }

        public IGameInningTeamBatter GetTestHomeRuneAtBat(int rBIs = 0)
        {
            TEST_SEQUENCE_TRACKER++;
            return new GameInningTeamBatterDto()
            {
                GameInningTeamAlternateKey = TEST_GAME_INNING_TEAM_ALTERNATE_KEY,
                Sequence = TEST_SEQUENCE_TRACKER,
                PlayerAlternateKey = GetPlayerAlternateKey(),
                EventType = (int)EventType.HomeRun,
                RBIs = rBIs,
                TargetEventType = (int)EventType.HomeRun
            };
        }

        private Guid GetPlayerAlternateKey()
        {
            switch (TEST_SEQUENCE_TRACKER)
            {
                case 1:
                    return TEST_PLAYER_ONE;
                case 2:
                    return TEST_PLAYER_TWO;
                case 3:
                    return TEST_PLAYER_THREE;
                case 4:
                    return TEST_PLAYER_FOUR;

                default:
                    return TEST_PLAYER_FIVE;
            }
        }

    }
}
