using System.Linq;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace AudioToggle.Playback
{
    [PluginName("Audio Toggle - Playback > Toggle Mute"), SupportedPlatform(PluginPlatform.Windows /*| PluginPlatform.Linux*/)]
    public class AudioTogglePlaybackMuteToggleBinding : AudioTogglePlaybackBase, IStateBinding, IBinding
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

        
        public void Press(TabletReference tablet, IDeviceReport report) => ToggleMute();

        public void Release(TabletReference tablet, IDeviceReport report)
        {
            return;
        }
    }
}
