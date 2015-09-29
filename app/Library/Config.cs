using System;
using System.Configuration;

namespace Library
{
    public enum PythonMode
    {
        Script,
        Module
    }

    public class Config
    {
        public static int RunSqlTimeout
        {
            get
            {
                var configTimeout = ConfigurationManager.AppSettings["RunSqlTimeout"];
                return configTimeout != null ? int.Parse(configTimeout) : 30;
            }
        }

        public static PythonMode PythonMode
        {
            get
            {
                var configMode = ConfigurationManager.AppSettings["PythonMode"];
                return configMode != null ? (PythonMode)Enum.Parse(typeof(PythonMode), configMode, true)
                    : PythonMode.Script;
            }
        }
    }
}
