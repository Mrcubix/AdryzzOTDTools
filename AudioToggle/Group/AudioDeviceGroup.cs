using AudioToggle.Entities;

namespace AudioToggle.Group
{
    public class AudioDeviceGroup
    {
        public AudioDeviceGroup(AudioDevice defautPlaybackDevice, AudioDevice defaultPlaybackCommunicationsDevice, AudioDevice defaultCaptureDevice, AudioDevice defaultCaptureCommunicationsDevice, int index)
        {
            DefaultPlaybackDevice = defautPlaybackDevice;
            DefaultPlaybackCommunicationsDevice = defaultPlaybackCommunicationsDevice;
            DefaultCaptureDevice = defaultCaptureDevice;
            DefaultCaptureCommunicationsDevice = defaultCaptureCommunicationsDevice;

            Index = index;
        }
 
        public AudioDevice DefaultPlaybackDevice { set; get; }
        public AudioDevice DefaultPlaybackCommunicationsDevice { set; get; }
        public AudioDevice DefaultCaptureDevice { set; get; }
        public AudioDevice DefaultCaptureCommunicationsDevice { set; get; }
        public int Index { set; get; }
        public bool IsMuted { set; get; } = false;

        public bool IsComplete()
        {
            return DefaultPlaybackDevice != null && DefaultPlaybackCommunicationsDevice != null && DefaultCaptureDevice != null && DefaultCaptureCommunicationsDevice != null;
        }

        public void SetAsDefault()
        {
            if (DefaultPlaybackDevice != null)
                DefaultPlaybackDevice.SetAsDefault();
            if (DefaultPlaybackCommunicationsDevice != null)
                DefaultPlaybackCommunicationsDevice.SetAsDefaultCommunications();
            if (DefaultCaptureDevice != null)
                DefaultCaptureDevice.SetAsDefault();
            if (DefaultCaptureCommunicationsDevice != null)
                DefaultCaptureCommunicationsDevice.SetAsDefaultCommunications();
        }

        public void ToggleMute()
        {
            if (DefaultPlaybackDevice != null)
                DefaultPlaybackDevice.ToggleMute();
            if (DefaultPlaybackCommunicationsDevice != null)
                DefaultPlaybackCommunicationsDevice.ToggleMute();
            if (DefaultCaptureDevice != null)
                DefaultCaptureDevice.ToggleMute();
            if (DefaultCaptureCommunicationsDevice != null)
                DefaultCaptureCommunicationsDevice.ToggleMute();
            
            IsMuted = !IsMuted;
        }

        public override string ToString()
        {
            string defaultPlaybackDevicePart = DefaultPlaybackDevice != null ? DefaultPlaybackDevice.ToString() : "null";
            string defaultPlaybackCommunicationsDevicePart = DefaultPlaybackCommunicationsDevice != null ? DefaultPlaybackCommunicationsDevice.ToString() : "null";
            string defaultCaptureDevicePart = DefaultCaptureDevice != null ? DefaultCaptureDevice.ToString() : "null";
            string defaultCaptureCommunicationsDevicePart = DefaultCaptureCommunicationsDevice != null ? DefaultCaptureCommunicationsDevice.ToString() : "null";
            return $"{Index} - ({defaultPlaybackDevicePart} & {defaultPlaybackCommunicationsDevicePart}) | ({defaultCaptureDevicePart} & {defaultCaptureCommunicationsDevicePart})";
        }
    }
}