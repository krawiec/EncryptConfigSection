using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace ecs_dotnet
{
    class CommandLineOptions
    {
        [Option(shortName: 's', longName: "section", HelpText = "Name of Section to encrypt", Required = true)]
        public string Section { get; set; }

        [Option(shortName: 'g', longName: "group", HelpText = "SectionGroup name where Section is placed in", Required = false)]
        public string SectionGroup { get; set; }

        [Option(shortName: 'c', longName: "config", HelpText = "Path to .config file", Required = true)]
        public string ConfigPath { get; set; }
    }
}
