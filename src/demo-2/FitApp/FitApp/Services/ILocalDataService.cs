using System;
using System.Collections.Generic;

namespace FitApp.Core
{
    public interface ILocalDataService
    {
        public void SaveSessionFromWeb(List<TrainingSession> sessions);
        public List<TrainingSession> GetLocalSessions();
        public void DeleteAllLocalSessions();
    }
}
