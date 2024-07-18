using HardmodeBiomeChanger.Common;
using System.IO;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace HardmodeBiomeChanger.Content.Items
{
    internal abstract class SpiritOverrider : ModItem
    {
        /// <summary>
        /// <see langword="true"/> means right.<br/>
        /// <see langword="false"/> means left.
        /// </summary>
        protected abstract bool HallowRight { get; }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.consumable = true;
            Item.UseSound = SoundID.Item119;
            Item.maxStack = 1;
            Item.ResearchUnlockCount = 3;
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(gold: 2)); // sell at 50 silver
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.FallenStar, 15);
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.PurificationPowder, 100);
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.VilePowder, 50);
                    player.QuickSpawnItem(Item.GetSource_Loot(), ItemID.ViciousPowder, 50);
                }
                else
                {
                    SpiritChoiceSystem.Instance.SetOverride(HallowRight);
                }
                return true;
            }
            return false;
        }
    }
}
