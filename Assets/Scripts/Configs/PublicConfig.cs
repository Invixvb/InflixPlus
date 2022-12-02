using System.Collections.Generic;
using ScriptableObjects;

namespace Configs
{
    public class PublicConfig
    {
        public readonly List<SO_Theme> themeList = new();

        public int themeSelectedIndex;

        public int levelSetting;
    }
}
