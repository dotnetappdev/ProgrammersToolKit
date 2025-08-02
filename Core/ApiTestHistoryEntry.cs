using System;
using ProgrammersToolKit.Core;
using System.Collections.Generic;

namespace ProgrammersToolKit.Core
{
    public class ApiTestHistoryEntry
    {
        public int Id { get; set; }
        public int ApiTestDefinitionId { get; set; }
        public DateTime RunDate { get; set; }
        public ApiTestResult Result { get; set; }
        public List<string> AssertionResults { get; set; }
    }
}
