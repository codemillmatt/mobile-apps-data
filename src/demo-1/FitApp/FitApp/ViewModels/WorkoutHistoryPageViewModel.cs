using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;

namespace FitApp.Core
{
    public class WorkoutHistoryPageViewModel : BaseViewModel
    {        
        IWebDataService cloudSvc;

        public WorkoutHistoryPageViewModel()
        {
            GetWorkoutHistoryCommand = new Command(async () => await ExecuteGetWorkoutHistoryCommand());
            StartNewWorkoutCommand = new Command(async () => await ExecuteStartWorkoutCommand());
            
            TrainingSessions = new ObservableCollection<TrainingSession>();
            
            cloudSvc = DependencyService.Get<IWebDataService>();
        }
       
        bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }

        ObservableCollection<TrainingSession> trainingSessions;
        public ObservableCollection<TrainingSession> TrainingSessions
        {
            get => trainingSessions;
            set => SetProperty(ref trainingSessions, value);
        }

        public ICommand GetWorkoutHistoryCommand { get; }
        public ICommand StartNewWorkoutCommand { get; }
        
        async Task ExecuteGetWorkoutHistoryCommand()
        {
            // update from the cloud            
            var trainingSessions = await cloudSvc.GetTrainingSessions();
            
            TrainingSessions.Clear();

            foreach (var session in trainingSessions)
            {
                var workoutDate = DateTime.Parse(session.RecordedOn);
                session.RecordedOnDisplay = workoutDate.ToString(@"MMMM dd @ hh:mm tt");
                TrainingSessions.Add(session);
            }

            IsRefreshing = false;
        }
        
        async Task ExecuteStartWorkoutCommand()
        {
            var workoutPage = new FitApp.Core.Pages.WorkoutPage();
            await App.Current.MainPage.Navigation.PushModalAsync(workoutPage);
        }
    }
}
