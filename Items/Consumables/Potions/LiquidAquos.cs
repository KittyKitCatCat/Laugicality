﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Items.Consumables.Potions
{
	public class LiquidAquos : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Critical Strike Chance\n5 minute duration");
        }
        public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 30;
			item.rare = ItemRarityID.Blue;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 2;
			item.UseSound = SoundID.Item3;
			item.consumable = true;
		}
        

        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("Aquos"), 5*60*60, true);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(null, "AquosDust", 1);
            recipe.AddIngredient(null, "ArcaneShard", 1);
            recipe.AddTile(null, "AlchemicalInfuser");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}