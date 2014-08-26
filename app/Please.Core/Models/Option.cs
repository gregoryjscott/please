using System;
using System.Text.RegularExpressions;

namespace Please.Core.Models
{
    public class Option<TTask>
    {
        public string Pattern { get; set; }
        public Action<TTask, Match> Action { get; set; }
        public Action<TTask, string> Action2 { get; set; }
    }
}