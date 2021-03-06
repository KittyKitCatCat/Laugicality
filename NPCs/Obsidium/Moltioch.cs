using Terraria;
using Terraria.ID;
using System.IO;

namespace Laugicality.NPCs.Obsidium
{
	class MoltiochHead : Moltioch
    {
		public override string Texture { get { return "Laugicality/NPCs/Obsidium/MoltiochHead"; } }

		public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.width = 96;
            npc.height = 96;
            npc.damage = 60;
            npc.defense = 12;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.buffImmune[24] = true;
        }

		public override void Init()
		{
			base.Init();
			head = true;
		}
        /*
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (LaugicalityWorld.obsidiumTiles > 150 && LaugicalityWorld.downedRagnar && Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0.25f;
            else return 0f;
        }*/

        int _attackCounter = 0;

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(_attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			_attackCounter = reader.ReadInt32();
		}

		public override void CustomBehavior()
		{

		}
	}

	class MoltiochBody : Moltioch
    {
		public override string Texture { get { return "Laugicality/NPCs/Obsidium/MoltiochBody"; } }

		public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.width = 96;
            npc.height = 96;
            npc.damage = 30;
            npc.defense = 38;
            npc.lifeMax = 500;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.buffImmune[24] = true;
        }
    }

	class MoltiochTail : Moltioch
    {
		public override string Texture { get { return "Laugicality/NPCs/Obsidium/MoltiochTail"; } }

		public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.width = 96;
            npc.height = 96;
            npc.damage = 40;
            npc.defense = 28;
            npc.lifeMax = 4000;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.buffImmune[24] = true;
        }

		public override void Init()
		{
			base.Init();
			tail = true;
		}
	}
    
	public abstract class Moltioch : Worm
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moltioch");
		}

		public override void Init()
		{
			minLength = 9;
			maxLength = 12;
			tailType = mod.NPCType<MoltiochTail>();
			bodyType = mod.NPCType<MoltiochBody>();
			headType = mod.NPCType<MoltiochHead>();
			speed = 20f;
			turnSpeed = 0.2f;
            npc.buffImmune[24] = true;
        }
        

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagmaticCrystal"));
        }
    }
    
}