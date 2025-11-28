namespace AlphaXSpreadSamplesExplorer;

/// <summary>
/// Interaction logic for SamplesSideBar.xaml
/// </summary>
public partial class SamplesSideBar : UserControl
{
    private readonly Dictionary<string, Type> _samples;

    public event EventHandler<SampleSelectedEventArgs> SampleSelected;

    public SamplesSideBar()
    {
        InitializeComponent();
        _samples = [];
        _lbSamples.ItemsSource = _samples;
        _lbSamples.Loaded += (s, e) => _lbSamples.SelectedIndex = 0;
    }

    public void RegisterSample(string header, Type sample) => _samples.Add(header, sample);

    private void OnSampleSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (KeyValuePair<string, Type>)_lbSamples.SelectedItem;
        SampleSelected?.Invoke(this, new SampleSelectedEventArgs(selectedItem.Value));
    }
}

public class SampleSelectedEventArgs(Type sample) : EventArgs
{
    public Type Sample { get; } = sample;
}
