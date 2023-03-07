using System.Collections.Generic;
using System.Linq;

namespace AudioToggle.Group
{
    public partial class AudioToggleGroupBase : AudioToggleBase
    {
        protected List<AudioDeviceGroup> Groups => Shared.Groups;
        protected AudioDeviceGroup Group { get; set; }
        protected int Index { get; set; }

        protected static Dictionary<string, int> PropertiesAndIndices = new Dictionary<string, int>()
        {
            { "Group 0", 0 },
            { "Group 1", 1 },
            { "Group 2", 2 }
        };

        protected static string[] Properties => PropertiesAndIndices.Keys.ToArray();

        public bool IsGroupAvailable()
        {
            if (Group == null)
                if (!UpdateGroup())
                    return false;

            return true;
        }

        public bool UpdateGroup()
        {
            if (Groups.Count <= Index)
                return false;

            Group = Groups.FirstOrDefault(g => g.Index == Index);

            if (Group == null)
                return false;

            return true;
        }
    }
}