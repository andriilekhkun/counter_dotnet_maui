using CommunityToolkit.Mvvm.Input;
using Counter_Lekhkun_Andrii.Controls;
using Counter_Lekhkun_Andrii.Views;
using System.Windows.Input;

namespace Counter_Lekhkun_Andrii
{
    public partial class MainPage : ContentPage
    {
        public ICommand IncreaseCommand { get; set; }
        public ICommand DecreaseCommand { get; set; }

        public MainPage()
        {
            InitializeComponent(); 
            
            IncreaseCommand = new RelayCommand(() => myCounter.CounterCount++);
            DecreaseCommand = new RelayCommand(() => myCounter.CounterCount--);

            BindingContext = this;
        }
    }
}
