using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;

namespace AudioToggle.Group
{
    [PluginName("Audio Toggle - Group > Switch Default Device"), SupportedPlatform(PluginPlatform.Windows /*| PluginPlatform.Linux*/)]
    public class AudioToggleGroupSwitchBinding : AudioToggleGroupBase, IStateBinding, IBinding
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

        public void Press(TabletReference tablet, IDeviceReport report) => SetDefault();

        public void Release(TabletReference tablet, IDeviceReport report)
        {
            return;
        }

        void SetDefault()
        {
            if (Toggle == null || Toggle.Disposed)
            {
                if (!UpdateToggle())
                {
                    Log.Write("AudioToggle", "Toggle not initialized", LogLevel.Error);
                    return;
                }
            }

            if (!IsGroupAvailable())
                return;

            Group.SetAsDefault();

            Log.Write("AudioToggle", $"Group {Group.Index} are now the default playback devices.", LogLevel.Info);
        }
    }
}
