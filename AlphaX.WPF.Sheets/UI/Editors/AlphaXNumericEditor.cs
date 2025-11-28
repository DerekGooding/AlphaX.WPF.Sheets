using System.Windows.Input;

namespace AlphaX.WPF.Sheets.UI.Editors
{
    internal class AlphaXNumericEditor : AlphaXEditorBase
    {
        public AlphaXNumericEditor()
        {
            BorderThickness = new Thickness();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            if(!string.IsNullOrEmpty(e.Text))
            {
                var character = e.Text[0];
                var ascii = (int)character;

                if (ascii == 46 && Text.Contains("."))
                {
                    e.Handled = true;
                }
                else if (ascii is (< 48 or > 57) and not 46)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
