using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace HardmodeBiomeChanger.Common
{
    internal class SpiritChoiceSystem : ModSystem
    {
        public static SpiritChoiceSystem Instance => ModContent.GetInstance<SpiritChoiceSystem>();
        private static readonly string msgKey = "Mods.HardmodeBiomeChanger.UseMessage";
        private static readonly Color msgColor = new(50, 255, 130);

        /// <summary>
        /// <see langword="true"/> means right.<br/>
        /// <see langword="false"/> means left.
        /// </summary>
        bool HallowRight;

        /// <summary>
        /// <see langword="false"/> will not change vanilla behavior.
        /// </summary>
        bool Override;

        internal void SetOverride(bool right)
        {
            HallowRight = right;
            Override = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = HardmodeBiomeChanger.Instance.GetPacket(2);
                packet.Write(Override);
                packet.Write(HallowRight);
                packet.Send();
            }
            else if(Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(msgKey), msgColor);
            }
            Main.NewText(Language.GetTextValue(msgKey), msgColor);
        }

        public override void Load()
        {
            IL_WorldGen.smCallBack += OverrideSpawn;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("override", Override);
            tag.Add("isRight", HallowRight);
        }
        public override void LoadWorldData(TagCompound tag)
        {
            Override = tag.GetBool("override");
            HallowRight = tag.GetBool("isRight");
        }

        internal void OverrideSpawn(ILContext il)
        {
            try
            {
                var cursor = new ILCursor(il);
                cursor.GotoNext(i => i.MatchBrtrue(out _));
                cursor.GotoNext(i => i.MatchBrtrue(out _));
                cursor.EmitDelegate<Func<int, int>>((prev) =>
                {
                    if (!Override) return prev;
                    if(HallowRight) return 0;
                    return 1;
                });
            }
            catch (Exception e)
            {
                // If there are any failures with the IL editing, this exception will dump the IL to Logs/ILDumps/{Mod Name}/{Method Name}.txt and close the game to prevent worldgen failures
                throw new ILPatchFailureException(ModContent.GetInstance<HardmodeBiomeChanger>(), il, e);
            }
        }
    }
}
