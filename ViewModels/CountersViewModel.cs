using CommunityToolkit.Mvvm.Input;
using Counter_Lekhkun_Andrii.Models;
using Counter_Lekhkun_Andrii.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Counter_Lekhkun_Andrii.ViewModels
{
    internal class CountersViewModel : IQueryAttributable
    {
        public ObservableCollection<CounterViewModel> AllCounters { get; }
        public ICommand GoToAddCommand { get; }
        public ICommand SelectCounterCommand { get; }

        public CountersViewModel()
        {
            AllCounters = new(Counter.LoadAll().Select(counter => new CounterViewModel(counter)));
            GoToAddCommand = new AsyncRelayCommand(GoToAddAsync);
            SelectCounterCommand = new AsyncRelayCommand<CounterViewModel>(SelectCounterAsync);
        }

        private async Task SelectCounterAsync(CounterViewModel? counter)
        {
            if (counter != null)
                await Shell.Current.GoToAsync($"{nameof(CounterPage)}?load={counter.Identifier}");
        }

        private async Task GoToAddAsync() =>
            await Shell.Current.GoToAsync($"{nameof(CounterPage)}");

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("saved"))
            {
                string? counterId = query["saved"].ToString();
                CounterViewModel? matchedCounter = AllCounters.Where(counter => counter.Identifier == counterId).FirstOrDefault();

                if (matchedCounter != null)
                {
                    matchedCounter.Reload();
                    AllCounters.Move(AllCounters.IndexOf(matchedCounter), 0);
                }
                else AllCounters.Insert(0, new CounterViewModel(Counter.Load(counterId)));
            } 
            else if (query.ContainsKey("deleted"))
            {
                string? counterId = query["deleted"].ToString();
                CounterViewModel? matchedCounter = AllCounters.Where(counter => counter.Identifier == counterId).FirstOrDefault();

                if (matchedCounter != null) AllCounters.Remove(matchedCounter);
            }
        }
    }
}
