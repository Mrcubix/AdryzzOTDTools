using System;
using AudioSwitcher.AudioApi.CoreAudio;

namespace AudioToggle.Entities
{
    public class AudioDevice
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public bool IsMuted { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDefaultCommunications { get; set; }
        public double Volume { get; set; }
        public object Device { get; set; }

        public AudioDevice(CoreAudioDevice device)
        {
            FullName = device.FullName;
            Name = device.Name;
            Id = device.Id;
            IsMuted = device.IsMuted;
            IsDefault = device.IsDefaultDevice;
            Volume = device.Volume;
            Device = device;
        }

        public AudioDevice(string fullName, string name, Guid id, bool isMuted, bool isDefault, double volume, object device)
        {
            FullName = fullName;
            Name = name;
            Id = id;
            IsMuted = isMuted;
            IsDefault = isDefault;
            Volume = volume;
            Device = device;
        }

        public void ToggleMute()
        {
            switch (Device)
            {
                case CoreAudioDevice dev:
                    if (IsControllerAvailable())
                    {
                        dev.ToggleMute();
                        IsMuted = dev.IsMuted;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetAsDefault()
        {
            switch (Device)
            {
                case CoreAudioDevice dev:
                    if (IsControllerAvailable())
                    {
                        dev.SetAsDefault();
                        IsDefault = dev.IsDefaultDevice;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void SetAsDefaultCommunications()
        {
            switch (Device)
            {
                case CoreAudioDevice dev:
                    if (IsControllerAvailable())
                    {
                        dev.SetAsDefaultCommunications();
                        IsDefaultCommunications = dev.IsDefaultCommunicationsDevice;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public bool IsControllerAvailable()
        {
            switch (Device)
            {
                case CoreAudioDevice dev:
                    return dev.Controller != null;
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}