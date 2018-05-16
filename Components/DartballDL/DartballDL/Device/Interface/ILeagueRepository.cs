using System;
using System.Collections.Generic;
using System.Text;
using static Dartball.DataLayer.Device.Repository.LeagueRepository;

namespace Dartball.DataLayer.Device.Interface
{
    public interface ILeagueRepository
    {

        List<League> LoadAll();
        League LoadByName(string name);

        void AddNew(League league);
        void Update(League league);
        void Save(League league);
        void Delete(string name);


    }
}
