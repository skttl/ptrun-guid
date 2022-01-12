using ManagedCommon;
using System.Collections.Generic;
using System.Windows.Forms;
using Wox.Plugin;

namespace skttl.Guid
{
    public class Main : IPlugin
    {
        private string IconPath { get; set; }

        private PluginInitContext Context { get; set; }
        public string Name => "Guid";

        public string Description => "Guid Generator";

        public List<Result> Query(Query query)
        {
            var guid = System.Guid.NewGuid().ToString();
            var guidUpper = guid.ToUpperInvariant();

            return new List<Result>
            {
                new Result
                {
                    Title = guid,
                    IcoPath = IconPath,
                    Action = e =>
                    {
                        Clipboard.SetText(guid);
                        return true;
                    },
                },

                new Result
                {
                    Title = guidUpper,
                    IcoPath = IconPath,
                    Action = e =>
                    {
                        Clipboard.SetText(guidUpper);
                        return true;
                    },
                },
            };
        }

        public void Init(PluginInitContext context)
        {
            Context = context;
            Context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(Context.API.GetCurrentTheme());
        }

        private void UpdateIconPath(Theme theme)
        {
            if (theme == Theme.Light || theme == Theme.HighContrastWhite)
            {
                IconPath = "images/guid.light.png";
            }
            else
            {
                IconPath = "images/guid.dark.png";
            }
        }

        private void OnThemeChanged(Theme currentTheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }
    }
}
