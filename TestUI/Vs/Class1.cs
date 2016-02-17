using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace EvilBaschdi.TestUI.Vs
{
    public interface ICoreProject
    {
        KeyValuePair<string, string> MahApps { get; }
        KeyValuePair<string, string> SysWinInt { get; }
    }

    public class CoreProject : ICoreProject
    {
        string _mahAppsInclude;
        string _mahAppsHintPath;
        string _sysWinIntInclude;
        string _sysWinIntHintPath;

        public CoreProject()
        {
            Load();
        }

        private void Load()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var configuration = currentDirectory.EndsWith("Release") ? "Release" : "Debug";
            var coreCsProj = currentDirectory.Replace($@"TestUI\bin\{configuration}", @"EvilBaschdi.Core\EvilBaschdi.Core.csproj");
            var coreProject = new Project(coreCsProj);
            foreach (var item in coreProject.Items)
            {
                if (item.EvaluatedInclude.StartsWith("MahApps.Metro"))
                {
                    _mahAppsInclude = item.EvaluatedInclude;
                    _mahAppsHintPath = item.Metadata.First().EvaluatedValue;
                }
                if (item.EvaluatedInclude.StartsWith("System.Windows.Interactivity"))
                {
                    _sysWinIntInclude = item.EvaluatedInclude;
                    _sysWinIntHintPath = item.Metadata.First().EvaluatedValue;
                }
            }
        }

        public KeyValuePair<string, string> MahApps
        {
            get { return new KeyValuePair<string, string>(_mahAppsInclude, _mahAppsHintPath); }
        }

        public KeyValuePair<string, string> SysWinInt
        {
            get { return new KeyValuePair<string, string>(_sysWinIntInclude, _sysWinIntHintPath); }
        }
    }

    public interface IChildProject

    {
    }

    public class ChildProject : IChildProject
    {
    }
}