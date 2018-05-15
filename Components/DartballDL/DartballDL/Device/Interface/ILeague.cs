using System;
using System.Collections.Generic;
using System.Text;
using static DartballDL.Device.Repository.LeagueRepository;

namespace DartballDL.Device.Interface
{
    public interface ILeague
    {

        List<League> LoadAll();
        League LoadByName(string name);

        void AddNew(League league);
        void Update(League league);
        void Save(League league);


    }
}
