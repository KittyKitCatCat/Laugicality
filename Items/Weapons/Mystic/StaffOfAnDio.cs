using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Items.Weapons.Mystic
{
	public class StaffOfAnDio : MysticItem
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Loki's Staff");
            Tooltip.SetDefault("'Many tricks up his sleeve'\nIllusion inflicts 'Chilled'\nFires different projectiles based on Mysticism");
            Item.staff[item.type] = true; 
        }

		public override void SetMysticDefaults()
		{
			item.damage = 40;
            item.width = 48;
			item.height = 48;
			item.useTime = 18;
			item.useAnimation = 28;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shootSpeed = 6f;
		}

        public override void Destruction(LaugicalityPlayer modPlayer)
        {
            item.damage = 52;
            item.useTime = 24;
            item.useAnimation = item.useTime;
            item.knockBack = 8;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("AnDioDestruction1");
            item.UseSound = SoundID.Item20;
            item.scale = 1f;
            LuxCost = 8;
        }

        public override void Illusion(LaugicalityPlayer modPlayer)
        {
            item.damage = 26;
            item.useTime = 2;
            item.useAnimation = item.useTime;
            item.knockBack = 5;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("AnDioIllusion");
            item.noUseGraphic = false;
            item.UseSound = SoundID.Item20;
            item.scale = 1f;
            VisCost = 1;
        }

        public override void Conjuration(LaugicalityPlayer modPlayer)
        {
            item.damage = 38;
            item.useTime = 30;
            item.useAnimation = item.useTime;
            item.knockBack = 2;
            item.shootSpeed = 18f;
            item.shoot = mod.ProjectileType("AnDioConjuration1");
            item.noUseGraphic = false;
            item.UseSound = SoundID.Item20;
            item.scale = 1f;
            MundusCost = 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DioritusCore", 1);
            recipe.AddIngredient(null, "AndesiaCore", 1);
            recipe.AddIngredient(3081, 25);
            recipe.AddIngredient(3086, 25);
            recipe.AddRecipeGroup("TitaniumBars", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}