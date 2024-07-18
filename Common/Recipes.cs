using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using HardmodeBiomeChanger.Content.Items;

namespace HardmodeBiomeChanger.Common
{
    internal class Recipes : ModSystem
    {
        private RecipeGroup evilPowders;
        private RecipeGroup ironOres;
        public override void AddRecipeGroups()
        {
            evilPowders = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.VilePowder)}", ItemID.VilePowder, ItemID.ViciousPowder);
            RecipeGroup.RegisterGroup(nameof(ItemID.VilePowder), evilPowders);
            ironOres = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.IronOre)}", ItemID.IronOre, ItemID.LeadOre);
            RecipeGroup.RegisterGroup(nameof(ItemID.IronOre), ironOres);
        }
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<SpiritOverriderLeft>())
                .AddCondition(Condition.PreHardmode)
                .AddTile(TileID.DemonAltar)
                .AddIngredient(ItemID.Glass, 10)
                .AddIngredient(ItemID.PurificationPowder, 100)
                .AddIngredient(ItemID.ArcaneCrystal, 5)
                .AddRecipeGroup(evilPowders, 100)
                .AddRecipeGroup(ironOres, 10)
                .AddCustomShimmerResult(ModContent.ItemType<SpiritOverriderRight>())
                .Register();

            Recipe.Create(ModContent.ItemType<SpiritOverriderRight>())
                .AddCondition(Condition.PreHardmode)
                .AddTile(TileID.DemonAltar)
                .AddIngredient(ItemID.Glass, 10)
                .AddRecipeGroup(evilPowders, 100)
                .AddIngredient(ItemID.ArcaneCrystal, 5)
                .AddIngredient(ItemID.PurificationPowder, 100)
                .AddRecipeGroup(ironOres, 10)
                .AddCustomShimmerResult(ModContent.ItemType<SpiritOverriderLeft>())
                .Register();
        }
    }
}
