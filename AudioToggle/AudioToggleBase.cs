using AudioToggle.Entities;
using OpenTabletDriver.Plugin;

namespace AudioToggle
{
    public partial class AudioToggleBase
    {
        protected IAudioToggle Toggle => Shared.Toggle;
        protected AudioDevice Device { get; set; }

        public virtual void ToggleMute()
        {
            if (Toggle == null || Toggle.Disposed)
            {
                if (!UpdateToggle())
                {
                    Log.Write("AudioToggle", "Toggle not initialized", LogLevel.Error);
                    return;
                }
            }

            if (Device == null)
            {
                Log.Write("AudioToggle", "Device not initialized, is it connected?", LogLevel.Error);
                return;
            }
            
            Device.ToggleMute();

            Log.Write("AudioToggle", $"Device '{Device.Name}' is now {(Device.IsMuted ? "Muted" : "Unmuted")}", LogLevel.Info);
        }

        public bool IsToggleAvailable()
        {
            if (Toggle == null || Toggle.Disposed)
                if (!UpdateToggle())
                    return false;

            return true;
        }

        public bool UpdateToggle()
        {
            Shared.UpdateToggle();
            return Toggle.Initialize();
        }
    }
}