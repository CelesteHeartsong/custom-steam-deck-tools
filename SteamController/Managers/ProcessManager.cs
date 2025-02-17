using System.Diagnostics;

namespace SteamController.Managers
{
    public sealed class ProcessManager : Manager
    {
        public static readonly String[] ActivationProcessNames = new String[]
        {
            "Playnite.FullscreenApp"
        };

        private bool activated;

        private Process? FindActivationProcess()
        {
            foreach (var processName in ActivationProcessNames)
            {
                var process = Process.GetProcessesByName(processName).FirstOrDefault();
                if (process is not null)
                    return process;
            }

            return null;
        }

        public override void Tick(Context context)
        {
            // React to state change
            if (FindActivationProcess() is not null)
            {
                if (!activated)
                {
                    activated = true;
                    context.ToggleDesktopMode(false);
                }
            }
            else
            {
                if (activated)
                {
                    activated = false;
                    context.ToggleDesktopMode(true);
                }
            }
        }
    }
}
