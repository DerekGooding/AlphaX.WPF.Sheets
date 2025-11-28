using AlphaX.Sheets.Workbook.WorkSheet;
using System;

namespace AlphaX.Sheets.EventArgs
{
    public class SheetEventArgs : EventArgs
    {
        public WorkSheet WorkSheet { get; }

        public SheetEventArgs(WorkSheet workSheet)
        {
            WorkSheet = workSheet;
        }
    }
}
