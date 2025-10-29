using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Counter_Lekhkun_Andrii.Models;

using System.Windows.Input;

namespace Counter_Lekhkun_Andrii.ViewModels
{
    internal class CounterViewModel : ObservableObject, IQueryAttributable
    {
        private Counter _counter;

        public string Count
        {
            get => _counter.Count;
            set 
            {
                if(_counter.Count != value)
                {
                    _counter.Count = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => _counter.Name;
            set
            {
                if (_counter.Name != value)
                {
                    _counter.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Color
        {
            get => _counter.Color;
            set
            {
                if (_counter.Color != value)
                {
                    _counter.Color = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Identifier => _counter.DataFilename;
        public string StartCount => _counter.StartCount;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        public CounterViewModel()
        {
            _counter = new Counter();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);

            IncreaseCommand = new RelayCommand(Increase);
            DecreaseCommand = new RelayCommand(Decrease);
            ResetCommand = new RelayCommand(ResetValue);
        }

        public CounterViewModel(Counter counter)
        {
            _counter = counter;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);

            IncreaseCommand = new RelayCommand(Increase);
            DecreaseCommand = new RelayCommand(Decrease);
            ResetCommand = new RelayCommand(ResetValue);
        }

        private async Task Save()
        {
            if (!int.TryParse(Count, out _)) 
                return;

            _counter.StartCount = Count;
            _counter.Color = Color.Equals(string.Empty) ? "LightGray" : Color;
            RefreshProperties();

            _counter.Save();
            await Shell.Current.GoToAsync($"..?saved={_counter.DataFilename}");
        }

        private async Task Delete()
        {
            _counter.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_counter.DataFilename}");
        }

        private void Increase()
        {
            if (int.TryParse(_counter.Count, out int value))
            {
                Count = (++value).ToString();
                _counter.Save();
            }
        }

        private void Decrease()
        {
            if (int.TryParse(_counter.Count, out int value))
            {
                Count = (--value).ToString();
                _counter.Save();
            }
        }

        private void ResetValue()
        {
            Count = StartCount;
            _counter.Save();
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _counter = Counter.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _counter = Counter.Load(_counter.DataFilename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Color));
        }
    }
}
