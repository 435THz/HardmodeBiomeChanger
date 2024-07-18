using HardmodeBiomeChanger.Common;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HardmodeBiomeChanger
{
	public class HardmodeBiomeChanger : Mod
	{
		public static HardmodeBiomeChanger Instance => ModContent.GetInstance<HardmodeBiomeChanger>();

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                if (reader.ReadBoolean())
                    SpiritChoiceSystem.Instance.SetOverride(reader.ReadBoolean());
            }
        }
    }
}
