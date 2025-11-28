namespace AlphaX.WPF.Sheets.Rendering;

public interface IRenderEngine
{
    /// <summary>
    /// Begins rendering.
    /// </summary>
    void BeginRender();
    /// <summary>
    /// Ends rendering.
    /// </summary>
    void EndRender();
    /// <summary>
    /// Draws grid lines on sheet.
    /// </summary>
    /// <param name="topRow"></param>
    /// <param name="bottomRow"></param>
    void DrawGridLines(int topRow, int leftCol, int bottomRow, int rightCol);
    /// <summary>
    /// Draws row header cells on sheet.
    /// </summary>
    void DrawRowHeaderCells(int topRow, int bottomRow);
    /// <summary>
    /// Draws column header cells on sheet.
    /// </summary>
    void DrawColumnHeaderCells(int leftCol, int rightCol);
    /// <summary>
    /// Draws cells on sheet.
    /// </summary>
    void DrawCellRange(int topRow, int leftColumn, int bottomRow, int rightColumn);
    /// <summary>
    /// Draws top left area on the sheet.
    /// </summary>
    void DrawTopLeft();
}