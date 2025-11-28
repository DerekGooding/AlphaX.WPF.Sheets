using AlphaX.WPF.Sheets.UI.Editors;

namespace AlphaX.WPF.Sheets.CellTypes
{
    public abstract class BaseCellType : ICellType
    {
        internal virtual void DrawCell(DrawingContext drawingContext, object value, Style style, IFormatter formatter, Rect cellRect, double pixelPerDip)
        {
            if (style.BackColor != AlphaX.Sheets.Drawing.Color.Transparent)
            {
                drawingContext.DrawRectangle(style.Background, null, cellRect);
            }
        }

        /// <summary>
        /// Gets the editor for cell type
        /// </summary>
        /// <returns></returns>
        public abstract AlphaXEditorBase GetEditor(Style style);
    }
}
