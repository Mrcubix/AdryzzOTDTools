using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace AudioToggle.Group
{
    [PluginName("Audio Toggle - Group > Toggle Mute"), SupportedPlatform(PluginPlatform.Windows /*| PluginPlatform.Linux*/)]
    public class AudioToggleGroupMuteToggleBinding : AudioToggleGroupBase, IStateBinding, IBinding
    {
        [Property("Device"), PropertyValidated(nameof(ValidProperties))]
        public string DeviceName
        {
            set
            {
                Index = PropertiesAndIndices[value];

                if (!IsGroupAvailable())
                    return;
            }
            get
            {
                if (Group != null)
                    return $"Group {Index}";
                else
                    return "Not Available";
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
