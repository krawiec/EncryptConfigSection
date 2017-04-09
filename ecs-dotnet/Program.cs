using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ecs_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            ClientSettingsSection section = null;
            ConfigurationSectionGroup group = null;
            var options = new CommandLineOptions();
            var isValid = CommandLine.Parser.Default.ParseArgumentsStrict(args, options);

            sb.Append(string.Format("Trying to encrypt Section {0} ", options.Section));
            if(string.IsNullOrEmpty(options.SectionGroup) == false)
            {
                sb.Append(string.Format("under {0} ", options.SectionGroup));
            }
            sb.Append(string.Format("in file {0}", options.ConfigPath));

            Console.WriteLine(sb.ToString());


            if( System.IO.File.Exists(options.ConfigPath) == false)
            {
                Console.WriteLine("File {0} does not exists!", options.ConfigPath);
                return;
            }

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = options.ConfigPath;

            // Get the current configuration file.
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            if (string.IsNullOrEmpty(options.SectionGroup) == false)
            {
                group = config.SectionGroups[options.SectionGroup];
                if (group == null)
                {
                    Console.WriteLine("Could not read SectionGroup {0}", options.SectionGroup);
                    return;
                }

                section = group.Sections[options.Section] as ClientSettingsSection;
                if (section == null)
                {
                    Console.WriteLine("Could not read Section {0}", options.Section);
                    return;
                }
            }
            else
            {
                section = config.Sections[options.Section] as ClientSettingsSection;
                if (section == null)
                {
                    Console.WriteLine("Could not read Section {0}", options.Section);
                    return;
                }
            }
            
            section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");

            section.SectionInformation.ForceSave = true;
            config.Save(ConfigurationSaveMode.Full);

            Console.WriteLine("Section {0} encrypted successfully!", options.Section);
        }
    }
}
