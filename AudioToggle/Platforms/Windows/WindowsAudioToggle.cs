﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
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
            controller.Dispose();
            Disposed = true;
        }

        public bool Initialize()
        {
            controller = new CoreAudioController();
            Initialized = true;
            return true;
        }

        public void ListDevices()
        {
            try
            {
                if (controller == null)
                {
                    Initialize();
                }
                var playback = controller.GetPlaybackDevices().ToArray();
                var capture = controller.GetCaptureDevices().ToArray();
                string p = "Playback devices:\n";
                for (int i = 0; i < playback.Length; i++)
                {
                    p += string.Format("Device {0}: {1}\n", i, playback[i].FullName);
                }

                string c = "Capture devices:\n";
                for (int i = 0; i < capture.Length; i++)
                {
                    c += string.Format("Device {0}: {1}\n", i, capture[i].FullName);
                }
                Log.Write("AudioToggle", p + c);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

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
            catch(Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void ToggleInputDevice(int index)
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

        public void SetDefaultOutputDevice(int index)
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
                    return;
                }
                else
                {
                    dev = controller.GetPlaybackDevices().ToArray()[index];
                }
                dev.SetAsDefault();
                Log.Write("AudioToggle", "Set device " + dev.FullName + " as default output device.", LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        public void SetDefaultInputDevice(int index)
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
                    return;
                }
                else
                {
                    dev = controller.GetCaptureDevices().ToArray()[index];
                }
                dev.SetAsDefault();
                Log.Write("AudioToggle", "Set device " + dev.FullName + " as default input device.", LogLevel.Debug);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }
    }
}
