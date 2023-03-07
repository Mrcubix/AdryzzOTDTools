using System.Collections.Generic;
using System.Linq;
using AudioToggle.Entities;

namespace AudioToggle.Playback
{
    public abstract class AudioTogglePlaybackBase : AudioToggleBase
    {
        protected List<AudioDevice> PlaybackDevices => Shared.PlaybackDevices;
        protected static string[] Properties = Shared.PlaybackDevices.Select(d => d.Name).ToArray();
    }
}