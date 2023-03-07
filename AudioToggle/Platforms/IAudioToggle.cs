using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioToggle.Entities;

namespace AudioToggle
{
    public interface IAudioToggle : IDisposable
    {
        bool Initialized { get; set; }
        bool Initialize();

        void ToggleOutputDevice(int index);
        void ToggleInputDevice(int index);

        void ChangeOutputDevice(int Standard, int Comms);
        void ChangeInputDevice(int Standard, int Comms);

        List<AudioDevice> GetPlaybackDevices();
        List<AudioDevice> GetCaptureDevices();

        bool Disposed { get; set; }
    }
}
