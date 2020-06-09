using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FitApp.Core
{
    public class WebDataService : IWebDataService
    {
        string syncRequestBase = "/trainingsession/sync";
        string saveRequestBase = "/trainingsession/record";

        static HttpClient client;

        public WebDataService()
        {
            client = new HttpClient();
        }

        public async Task GetTrainingSessions()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
                return;

            var syncRequestUrl = $"{Constants.WebServerBaseUrl}{syncRequestBase}";

            try
            {
                // create the sync request
                var userId = Preferences.Get(Constants.UserIdPreference, "Matt");

                var syncRequest = new SyncRequest { FromVersion = 0, UserId = userId };

                // perform the request
                var request = new HttpRequestMessage(HttpMethod.Post, syncRequestUrl);

                request.Content = new StringContent(JsonConvert.SerializeObject(syncRequest), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                // pull out the info
                var syncResultJson = await response.Content.ReadAsStringAsync();
                var syncResult = JsonConvert.DeserializeObject<DataSyncResult>(syncResultJson);

                // save training sessions locally
                var localData = DependencyService.Get<ILocalDataService>(DependencyFetchTarget.GlobalInstance);

                localData.SaveSessionFromWeb(syncResult.TrainingData);                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);                
            }
        }

        public async Task<bool> SaveTrainingSession(TrainingSessionRequest session)
        {
            try
            {
                var saveRequestUrl = $"{Constants.WebServerBaseUrl}{saveRequestBase}";

                // perform the request
                var request = new HttpRequestMessage(HttpMethod.Post, saveRequestUrl);

                request.Content = new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                return true;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                return false;
            }
        }
    }
}
