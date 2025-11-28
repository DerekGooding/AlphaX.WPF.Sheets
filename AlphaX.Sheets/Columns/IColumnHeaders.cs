using AlphaX.Sheets.Core;

namespace AlphaX.Sheets.Columns.Columns
{
    public interface IColumnHeaders : IHeaders
    {
        /// <summary>
        /// Gets or sets the header row count.
        /// </summary>
        int RowCount { get; set; }
        /// <summary>
        /// Gets or sets the default row height.
        /// </summary>
        int DefaultRowHeight { get; set; }
        /// <summary>
        /// Gets the column headers height.
        /// </summary>
        double Height { get; }
    }
}
