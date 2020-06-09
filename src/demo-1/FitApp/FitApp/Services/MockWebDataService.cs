using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitApp.Core
{
    public class MockWebDataService : IWebDataService
    {
        public MockWebDataService()
        {
        }

        public async Task<List<TrainingSession>> GetTrainingSessions()
        {
            var sessions = new List<TrainingSession>
            {
                                new TrainingSession { RecordedOn = DateTime.Now.ToShortDateString(), Steps = 500, Distance = 5},
                                new TrainingSession { RecordedOn = DateTime.Now.ToShortDateString(), Steps = 200, Distance = 2}
                            };

            return await Task.FromResult(sessions);
        }    

        public async Task<bool> SaveTrainingSession(TrainingSessionRequest session)
        {
            return await Task.FromResult(true);
        }
    }
}



