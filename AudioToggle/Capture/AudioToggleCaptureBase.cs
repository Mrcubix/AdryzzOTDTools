using System.Collections.Generic;
using System.Linq;
using AudioToggle.Entities;
using OpenTabletDriver.Plugin;

namespace AudioToggle.Capture
{
    public partial class AudioToggleCaptureBase : AudioToggleBase
    {
        protected List<AudioDevice> CaptureDevices => Shared.CaptureDevices;

        protected static string[] Properties = Shared.CaptureDevices.Select(d => d.Name).ToArray();

        public override void ToggleMute()
        {
            if (Toggle == null || Toggle.Disposed)
            {
                Log.Write("AudioToggle", "Tool instance not initialized", LogLevel.Error);
                return;
            }

            if (Device == null)
            {
                Log.Write("AudioToggle", "Device not initialized, is it connected?", LogLevel.Error);
                return;
            }
            
            Device.ToggleMute();

            Log.Write("AudioToggle", $"Device '{Device.Name}' is now {(Device.IsMuted ? "Muted" : "Unmuted")}", LogLevel.Info);
        }
    }
}