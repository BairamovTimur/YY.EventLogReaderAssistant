﻿using System.IO;
using Xunit;

namespace YY.EventLogReaderAssistant.Tests
{
    public class LogParserLGFTests
    {
        #region Private Member Variables

        private readonly string sampleDataDirectory;
        private readonly string sampleDatabaseFile;

        #endregion

        #region Constructor

        public LogParserLGFTests()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            sampleDataDirectory = Path.Combine(currentDirectory, "SampleData");
            sampleDatabaseFile = Path.Combine(sampleDataDirectory, "LGFFormatEventLog", "1Cv8.lgf");
        }

        #endregion

        #region Public Methods

        [Fact]
        public void ItsBeginOfEvent_Test()
        {
            string sourceString =
                "{20200412134348,N," +
                "{ 0,0},1,1,1,1,1,I,\"\",0," +
                "{ \"U\"},\"\",1,1,0,1,0," +
                "{ 0}" +
                "}";

            bool itsBeginOfEvent = LogParserLGF.ItsBeginOfEvent(sourceString);

            Assert.True(itsBeginOfEvent);
        }

        [Fact]
        public void ItsEndOfEvent_Test()
        {
            int countBracket = 0;
            bool textBlockOpen = false;
            string sourceData =
                "{20200412134348,N,\n" +
                "{ 0,0},1,1,1,1,1,I,\"\",0,\n" +
                "{ \"U\"},\"\",1,1,0,1,0,\n" +
                "{ 0}\n" +
                "}";

            string[] sourceDataArray = sourceData.Split('\n');

            bool itsEndOfEvent = false;
            int lineNumber;
            for (lineNumber = 0; lineNumber < sourceDataArray.Length; lineNumber++)
            {
                string currentString = sourceDataArray[lineNumber];
                itsEndOfEvent = LogParserLGF.ItsEndOfEvent(currentString, ref countBracket, ref textBlockOpen);
            }                

            Assert.True(itsEndOfEvent);
            Assert.Equal(sourceDataArray.Length, lineNumber);
        }

        [Fact]
        public void ReadEventLogReferences_Test()
        {
            bool dataExists = false;

            using (EventLogReader reader = EventLogReader.CreateReader(sampleDatabaseFile))
            {
                dataExists =
                    reader.Applications.Count > 0
                    && reader.Events.Count > 0
                    && reader.WorkServers.Count > 0;
            }

            Assert.True(dataExists);
        }

        #endregion
    }
}
