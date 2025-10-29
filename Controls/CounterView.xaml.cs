using System.Windows.Input;

namespace Counter_Lekhkun_Andrii.Controls;

public partial class CounterView : ContentView
{
    public static readonly BindableProperty CounterNameProperty = BindableProperty.Create(nameof(CounterName), typeof(string), typeof(CounterView), "No Name");
    public static readonly BindableProperty CounterCountProperty = BindableProperty.Create(nameof(CounterCount), typeof(int), typeof(CounterView), 0);
    public static readonly BindableProperty IncreaseCommandProperty = BindableProperty.Create(nameof(IncreaseCommand), typeof(ICommand), typeof(CounterView));
    public static readonly BindableProperty DecreaseCommandProperty = BindableProperty.Create(nameof(DecreaseCommand), typeof(ICommand), typeof(CounterView));

    public string CounterName
    {
        get => (string)GetValue(CounterNameProperty);
        set => SetValue(CounterNameProperty, value);
    }

    public int CounterCount
    {
        get => (int)GetValue(CounterCountProperty);
        set => SetValue(CounterCountProperty, value);
    }

    public ICommand IncreaseCommand
    {
        get => (ICommand)GetValue(IncreaseCommandProperty);
        set => SetValue(IncreaseCommandProperty, value);
    }

    public ICommand DecreaseCommand
    {
        get => (ICommand)GetValue(DecreaseCommandProperty);
        set => SetValue(DecreaseCommandProperty, value);
    }

    public CounterView()
    {
        InitializeComponent();
    }
}