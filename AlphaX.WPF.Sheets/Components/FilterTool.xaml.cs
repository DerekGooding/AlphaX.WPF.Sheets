using AlphaX.Sheets.Filtering;

namespace AlphaX.WPF.Sheets.Components
{
    /// <summary>
    /// Interaction logic for FilterTool.xaml
    /// </summary>
    public partial class FilterTool : UserControl
    {
        private FilterProvider _filterProvider;

        internal FilterTool(FilterProvider filterProvider)
        {
            InitializeComponent();
            _filterProvider = filterProvider;
        }
    }
}
