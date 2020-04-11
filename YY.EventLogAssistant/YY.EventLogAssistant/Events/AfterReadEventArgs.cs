﻿using YY.EventLogAssistant.Models;
using System;

namespace YY.EventLogAssistant
{
    public sealed class AfterReadEventArgs : EventArgs
    {
        public AfterReadEventArgs(RowData rowData, long eventNumber)
        {
            RowData = rowData;
            EventNumber = eventNumber;
        }

        public RowData RowData { get; }
        public long EventNumber { get; }
    }    
}