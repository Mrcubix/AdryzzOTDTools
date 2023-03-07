using System;
using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioToggle.Entities;
using OpenTabletDriver.Plugin;

namespace AudioToggle.Platforms.Windows
{
    class WindowsAudioToggle : IAudioToggle
    {
        public bool Initialized { get; set; }
        public bool Disposed { get; set; }

        CoreAudioController controller;

        public void Dispose()
        {
            controller?.Dispose();
            Disposed = true;
        }

        public bool Initialize()
        {
            Disposed = false;
            controller = new CoreAudioController();
            Initialized = true;
            return true;
        }

        public List<AudioDevice> GetPlaybackDevices()
        {
            if (controller == null || !TrueIsControllerDisposed())
            {
                Initialize();
            }

            return controller?.GetPlaybackDevices()?.ToList().Select(d => new AudioDevice(d)).ToList();
        }

        public List<AudioDevice> GetCaptureDevices()
        {
            if (controller == null || !TrueIsControllerDisposed())
            {
                Initialize();
            }

            return controller?.GetCaptureDevices()?.Select(d => new AudioDevice(d)).ToList();
        }

        public bool TrueIsControllerDisposed()
        {
            if (controller == null)
                return true;
                
            try
            {
                controller.GetDefaultDevice(DeviceType.Playback, Role.Communications);
                return false;
            }
            catch (ObjectDisposedException)
            {
                Log.Write("AudioToggle", "The controller is disposed, it should be initialized soon.", LogLevel.Debug);
                return true;
            }
        }

        /// <summary>
        /// Toggle mute for the specified output device
        /// </summary>
        public void ToggleOutputDevice(int index)
        {
            try
            {
                if (controller == null)
                {
                    Initialize();
                }

                CoreAudioDevice dev;

                if (index < 0)
                {
                    dev = controller.DefaultPlaybackDevice;
                }
                else
                {
                    dev = controller.GetPlaybackDevices().ToArray()[index];
                }

                dev.ToggleMute();
                Log.Write("AudioToggle", "Toggle mute for " + dev.FullName, LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void ToggleInputDevice(int index)
        {
            try
            {
                if (controller == null && !Disposed)
                {
                    Initialize();
                }

                CoreAudioDevice dev;

                if (index < 0)
                {
                    dev = controller.DefaultCaptureDevice;
                }
                else
                {
                    dev = controller.GetCaptureDevices().ToArray()[index];
                }

                dev.ToggleMute();

                Log.Write("AudioToggle", "Toggle mute for " + dev.FullName, LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>
        /// Change the default output device
        /// </summary>
        public void ChangeOutputDevice(int Standard, int Comms)
        {
            try
            {
                if (controller == null && !Disposed)
                {
                    Initialize();
                }

                CoreAudioDevice devStandard;
                CoreAudioDevice devComms;

                if (Standard < 0)
                {
                    devStandard = controller.DefaultPlaybackDevice;
                }
                else
                {
                    devStandard = controller.GetPlaybackDevices().ToArray()[Standard];
                }
                if (Comms < 0)
                {
                    devComms = controller.DefaultPlaybackDevice;
                }
                else
                {
                    devComms = controller.GetPlaybackDevices().ToArray()[Comms];
                }

                devStandard.SetAsDefault();
                devComms.SetAsDefaultCommunications();

                Log.Write("AudioToggle", "Set standard output device to " + devStandard.FullName, LogLevel.Debug);
                Log.Write("AudioToggle", "Set comms output device to " + devComms.FullName, LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
        public void ChangeInputDevice(int Standard, int Comms)
        {
            try
            {
                if (controller == null)
                {
                    Initialize();
                }

                CoreAudioDevice devStandard;
                CoreAudioDevice devComms;

                if (Standard < 0)
                {
                    devStandard = controller.DefaultCaptureDevice;
                }
                else
                {
                    devStandard = controller.GetCaptureDevices().ToArray()[Standard];
                }
                if (Comms < 0)
                {
                    devComms = controller.DefaultCaptureDevice;
                }
                else
                {
                    devComms = controller.GetCaptureDevices().ToArray()[Comms];
                }

                devStandard.SetAsDefault();
                devComms.SetAsDefaultCommunications();

                Log.Write("AudioToggle", "Set standard input device to " + devStandard.FullName, LogLevel.Debug);
                Log.Write("AudioToggle", "Set comms input device to " + devComms.FullName, LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }
}
