using Laugicality.NPCs.Bosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.Items.Loot
{
    public class AnnihilatorTreasureBag : LaugicalityItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 30;
            item.maxStack = 20;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = ItemRarityID.Purple;
            item.expert = true;
            bossBagNPC = mod.NPCType<TheAnnihilator>();
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("SteamBar"), Main.rand.Next(20, 35));
            player.QuickSpawnItem(mod.ItemType("SoulOfThought"), Main.rand.Next(25, 40));
            player.QuickSpawnItem(mod.ItemType("CogOfKnowledge"), 1);
            player.QuickSpawnItem(499, Main.rand.Next(10, 15));
        }
        
    }
}