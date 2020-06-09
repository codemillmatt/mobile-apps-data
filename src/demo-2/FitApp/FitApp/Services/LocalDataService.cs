using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache.SQLite;

namespace FitApp.Core
{
    public class LocalDataService : ILocalDataService
    {
        public LocalDataService()
        {
        }

        public void SaveSessionFromWeb(List<TrainingSession> sessions)
        {            
            Barrel.Current.EmptyAll();

            foreach (var item in sessions)
            {                
                Barrel.Current.Add(item.Id.ToString(), item, TimeSpan.FromDays(100));
            }                        
        }

        public List<TrainingSession>GetLocalSessions()
        {
            List<TrainingSession> localSessions = new List<TrainingSession>();

            var allIds = Barrel.Current.GetKeys(MonkeyCache.CacheState.Active);

            foreach (var id in allIds)
            {
                localSessions.Add(Barrel.Current.Get<TrainingSession>(id));
            }

            return localSessions;
        }

        public void DeleteAllLocalSessions()
        {
            Barrel.Current.EmptyAll();
        }
    }
}
