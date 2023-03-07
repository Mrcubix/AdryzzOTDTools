using System.Linq;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace AudioToggle.Playback
{
    [PluginName("Audio Toggle - Playback > Switch Default Device"), SupportedPlatform(PluginPlatform.Windows /*| PluginPlatform.Linux*/)]
    public class AudioTogglePlaybackSwitchBinding : AudioTogglePlaybackBase, IStateBinding, IBinding
    {
        [Property("Device"), PropertyValidated(nameof(ValidProperties))]
        public string DeviceName
        {
            set
            {
                Device = PlaybackDevices.FirstOrDefault(d => d.Name == value);
            }
            get
            {
                return Device.Name;
            }
        }

        public static string[] ValidProperties => Properties;
        

        public void Press(TabletReference tablet, IDeviceReport report) => setDefault();

        public void Release(TabletReference tablet, IDeviceReport report)
        {
            return;
        }

        void setDefault()
        {
            if (Toggle == null || Toggle.Disposed)
            {
                if (!UpdateToggle())
                {
                    Log.Write("AudioToggle", "Toggle not initialized", LogLevel.Error);
                    return;
                }
            }

            Device.SetAsDefault();

            Log.Write("AudioToggle", $"Device '{Device.Name}' is now the default playback device.", LogLevel.Info);
        }
    }
}
