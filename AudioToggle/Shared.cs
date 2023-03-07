using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioToggle.Entities;
using AudioToggle.Group;
using AudioToggle.Platforms.Linux;
using AudioToggle.Platforms.Windows;
using OpenTabletDriver.Desktop.Interop;
using OpenTabletDriver.Plugin;

namespace AudioToggle
{
    public class Shared
    {
        public static IAudioToggle Toggle { get; set; } = DesktopInterop.CurrentPlatform switch
        {
            PluginPlatform.Windows => new WindowsAudioToggle(),
            PluginPlatform.Linux => new LinuxAudioToggle(),
            _ => null
        };
        public static List<AudioDevice> PlaybackDevices => Toggle.GetPlaybackDevices();
        public static List<AudioDevice> CaptureDevices => Toggle.GetCaptureDevices();
        public static List<AudioDevice> AllDevices 
        {
            get
            {
                var devices = PlaybackDevices;
                devices.AddRange(CaptureDevices);
                return devices;
            }
        }
        public static List<AudioDeviceGroup> Groups => AudioToggleTool.DeviceGroups;

        public static void UpdateToggle()
        {
            Toggle = DesktopInterop.CurrentPlatform switch
            {
                PluginPlatform.Windows => new WindowsAudioToggle(),
                PluginPlatform.Linux => new LinuxAudioToggle(),
                _ => null
            };
        }
    }
}