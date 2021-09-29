using DI.Library.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DI.Blowout.Main
{
    public class Configuration : IPluginConfiguration
    {
        [XmlAttribute("BlowoutFrequency")]
        public float BlowoutFrequency;

        [XmlAttribute("DelayBeforeBlowout")]
        public float DelayBeforeBlowout;

        [XmlAttribute("BlowoutDuration")]
        public float BlowoutDuration;

        [XmlAttribute("DamageFrequency")]
        public float DamageFrequency;

        [XmlAttribute("DamagePerTick")]
        public byte DamagePerTick;

        [XmlAttribute("BlowoutUI_ID")]
        public ushort BlowoutUI_ID;

        [XmlArrayItem("ID")]
        public List<ushort> PreparingUI_IDs;

        [XmlArrayItem("ID")]
        public List<ushort> EndingUI_IDs;

        public void LoadDefaults()
        {
            BlowoutFrequency = 1200f;
            DelayBeforeBlowout = 30f;
            BlowoutDuration = 171f;
            DamageFrequency = 1f;

            DamagePerTick = 20;

            BlowoutUI_ID = 15001;

            PreparingUI_IDs = new List<ushort>()
            {
                15002,
                15003
            };

            EndingUI_IDs = new List<ushort>()
            {
                15004,
                15005
            };
        }
    }
}
