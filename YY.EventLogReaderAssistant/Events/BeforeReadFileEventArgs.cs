﻿using System;

namespace YY.EventLogReaderAssistant
{
    public sealed class BeforeReadFileEventArgs : EventArgs
    {
        public BeforeReadFileEventArgs(string fileName)
        {
            FileName = fileName;
            Cancel = false;
        }

        public string FileName { get; }
        public bool Cancel { get; set; }
    }
}
