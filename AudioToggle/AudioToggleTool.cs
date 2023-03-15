using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AudioToggle.Entities;
using AudioToggle.Group;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.Attributes;
using Eto.Forms;
/*using AudioToggle.UX.Dialogs;
using OpenTabletDriver.Desktop.Interop;*/
using System;

namespace AudioToggle
{
    [PluginName("Audio Toggle - Configuration"), SupportedPlatform(PluginPlatform.Windows /*| PluginPlatform.Linux*/)]
    public class AudioToggleTool : ITool
    {
        public bool Initialize()
        {
            new Thread(new ThreadStart(InitializePlugin)).Start();
            return true;
        }

        public void InitializePlugin()
        {
            try
            {
                Shared.Toggle.Initialize();

                Log.Write("AudioToggle", "Controller Initialized", LogLevel.Debug);

                Log.Write("AudioToggle", "Playback Devices:", LogLevel.Debug);

                foreach (var device in Shared.PlaybackDevices)
                    Log.Write("AudioToggle", $"- Device: {device.Name}", LogLevel.Debug);

                Log.Write("AudioToggle", "Capture Devices:", LogLevel.Debug);

                foreach (var device in Shared.CaptureDevices)
                    Log.Write("AudioToggle", $"- Device: {device.Name}", LogLevel.Debug);

                InitializeGroups();

                Log.Write("AudioToggle", "Device Groups:", LogLevel.Debug);

                foreach (var group in DeviceGroups)
                    Log.Write("AudioToggle", $"- {group}", LogLevel.Debug);
            }
            catch (Exception e)
            {
                Log.Write("AudioToggle", $"Error initializing plugin: {e}", LogLevel.Error);
            }
        }

        public void Dispose()
        {
            Log.Debug("AudioToggle", "Disposing Controller");
            Shared.Toggle.Dispose();
            return;
        }

        public void InitializeGroups()
        {
            DeviceGroups.Clear();

            for (int i = 0; i < 3; i++)
                InitializeGroup(i);
        }

        public void InitializeGroup(int index)
        {
            switch (index)
            {
                case 0:
                    UpdateGroup(index, _group0DefaultPlaybackDevice, _group0DefaultPlaybackComDevice, _group0DefaultCaptureDevice, _group0DefaultCaptureComDevice);
                    break;
                case 1:
                    UpdateGroup(index, _group1DefaultPlaybackDevice, _group1DefaultPlaybackComDevice, _group1DefaultCaptureDevice, _group1DefaultCaptureComDevice);
                    break;
                case 2:
                    UpdateGroup(index, _group2DefaultPlaybackDevice, _group2DefaultPlaybackComDevice, _group2DefaultCaptureDevice, _group2DefaultCaptureComDevice);
                    break;
            }
        }

        public void UpdateGroup(int index, AudioDevice defaultPlaybackDevice, AudioDevice defaultPlaybackComDevice, AudioDevice defaultCaptureDevice, AudioDevice defaultCaptureComDevice)
        {
            if (DeviceGroups.Count > index)
                try
                {
                    DeviceGroups[index] = new AudioDeviceGroup(defaultPlaybackDevice,
                                                               defaultPlaybackComDevice,
                                                               defaultCaptureDevice,
                                                               defaultCaptureComDevice,
                                                               index);
                }
                catch (IndexOutOfRangeException)
                {
                    Log.Write("AudioToggle", $"Error updating group {index}: Index out of range, somehow", LogLevel.Error);
                }
            else
                DeviceGroups.Add(new AudioDeviceGroup(defaultPlaybackDevice,
                                                      defaultPlaybackComDevice,
                                                      defaultCaptureDevice,
                                                      defaultCaptureComDevice,
                                                      index));
        }

        private Application _app;

        public static List<AudioDeviceGroup> DeviceGroups { get; set; } = new List<AudioDeviceGroup>();

        // Group 1 ---------------------------------------------------------------------------------------------

        [Property("Group 0 - Default Playback Device")]
        public string Group0DefaultPlaybackDevice
        {
            set
            {
                _group0DefaultPlaybackDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
            }
            get
            {
                return _group0DefaultPlaybackDevice.Name;
            }
        }
        private AudioDevice _group0DefaultPlaybackDevice { get; set; }

        [Property("Group 0 - Default Playback Communication Device")]
        public string Group0DefaultPlaybackComDevice
        {
            set
            {
                _group0DefaultPlaybackComDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
            }
            get
            {
                return _group0DefaultPlaybackComDevice.Name;
            }
        }
        private AudioDevice _group0DefaultPlaybackComDevice { get; set; }

        [Property("Group 0 - Default Capture Device")]
        public string Group0DefaultCaptureDevice
        {
            set
            {
                _group0DefaultCaptureDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
            }
            get
            {
                return _group0DefaultCaptureDevice.Name;
            }
        }
        private AudioDevice _group0DefaultCaptureDevice { get; set; }

        [Property("Group 0 - Default Capture Communication Device")]
        public string Group0DefaultCaptureComDevice
        {
            set
            {
                    _group0DefaultCaptureComDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
            }
            get
            {
                return _group0DefaultCaptureComDevice.Name;
            }
        }
        private AudioDevice _group0DefaultCaptureComDevice { get; set; }

        // Group 2 ---------------------------------------------------------------------------------------------

        [Property("Group 1 - Playback Default Device")]
        public string Group1DefaultPlaybackDevice
        {
            set
            {
                _group1DefaultPlaybackDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(0, _group0DefaultPlaybackDevice, _group0DefaultPlaybackComDevice, _group0DefaultCaptureDevice, _group0DefaultCaptureComDevice);
            }
            get
            {
                return _group1DefaultPlaybackDevice.Name;
            }
        }
        private AudioDevice _group1DefaultPlaybackDevice { get; set; }

        [Property("Group 1 - Default Playback Communication Device")]
        public string Group1DefaultPlaybackComDevice
        {
            set
            {
                _group1DefaultPlaybackComDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(1, _group1DefaultPlaybackDevice, _group1DefaultPlaybackComDevice, _group1DefaultCaptureDevice, _group1DefaultCaptureComDevice);
            }
            get
            {
                return _group1DefaultPlaybackComDevice.Name;
            }
        }
        private AudioDevice _group1DefaultPlaybackComDevice { get; set; }

        [Property("Group 1 - Default Capture Device")]
        public string Group1DefaultCaptureDevice
        {
            set
            {
                _group1DefaultCaptureDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(1, _group1DefaultPlaybackDevice, _group1DefaultPlaybackComDevice, _group1DefaultCaptureDevice, _group1DefaultCaptureComDevice);
            }
            get
            {
                return _group1DefaultCaptureDevice.Name;
            }
        }
        private AudioDevice _group1DefaultCaptureDevice { get; set; }

        [Property("Group 1 - Default Capture Communication Device")]
        public string Group1DefaultCaptureComDevice
        {
            set
            {
                _group1DefaultCaptureComDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(1, _group1DefaultPlaybackDevice, _group1DefaultPlaybackComDevice, _group1DefaultCaptureDevice, _group1DefaultCaptureComDevice);
            }
            get
            {
                return _group1DefaultCaptureComDevice.Name;
            }
        }
        private AudioDevice _group1DefaultCaptureComDevice { get; set; }

        // Group 3 ---------------------------------------------------------------------------------------------

        [Property("Group 2 - Default Playback Device")]
        public string Group2DefaultPlaybackDevice
        {
            set
            {
                _group2DefaultPlaybackDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(2, _group2DefaultPlaybackDevice, _group2DefaultPlaybackComDevice, _group2DefaultCaptureDevice, _group2DefaultCaptureComDevice);
            }
            get
            {
                return _group2DefaultPlaybackDevice.Name;
            }
        }
        private AudioDevice _group2DefaultPlaybackDevice { get; set; }

        [Property("Group 2 - Default Playback Communication Device")]
        public string Group2DefaultPlaybackComDevice
        {
            set
            {
                _group2DefaultPlaybackComDevice = Shared.PlaybackDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(2, _group2DefaultPlaybackDevice, _group2DefaultPlaybackComDevice, _group2DefaultCaptureDevice, _group2DefaultCaptureComDevice);
            }
            get
            {
                return _group2DefaultPlaybackComDevice.Name;
            }
        }
        private AudioDevice _group2DefaultPlaybackComDevice { get; set; }

        [Property("Group 2 - Default Capture Device")]
        public string Group2DefaultCaptureDevice
        {
            set
            {
                _group2DefaultCaptureDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(2, _group2DefaultPlaybackDevice, _group2DefaultPlaybackComDevice, _group2DefaultCaptureDevice, _group2DefaultCaptureComDevice);
            }
            get
            {
                return _group2DefaultCaptureDevice.Name;
            }
        }
        private AudioDevice _group2DefaultCaptureDevice { get; set; }

        [Property("Group 2 - Default Capture Communication Device")]
        public string Group2DefaultCaptureComDevice
        {
            set
            {
                _group2DefaultCaptureComDevice = Shared.CaptureDevices?.FirstOrDefault(d => d.Name == value);
                UpdateGroup(2, _group2DefaultPlaybackDevice, _group2DefaultPlaybackComDevice, _group2DefaultCaptureDevice, _group2DefaultCaptureComDevice);
            }
            get
            {
                return _group2DefaultCaptureComDevice.Name;
            }
        }
        private AudioDevice _group2DefaultCaptureComDevice { get; set; }
    }
}