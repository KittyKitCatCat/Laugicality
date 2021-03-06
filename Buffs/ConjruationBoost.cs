using Terraria;
using Terraria.ModLoader;

namespace Laugicality.Buffs
{
	public class ConjurationBoost : LaugicalityBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Conjuration Power");
			Description.SetDefault("'Otherworldly energy strengthens you.'\n+50% Overflow");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<LaugicalityPlayer>(mod).GlobalOverflow += .5f;
        }
        
	}
}
