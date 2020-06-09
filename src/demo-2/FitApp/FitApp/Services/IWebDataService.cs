using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitApp.Core
{
    public interface IWebDataService
    {
        public Task<List<TrainingSession>> GetTrainingSessions();
        public Task<bool> SaveTrainingSession(TrainingSessionRequest session);

    }
}
