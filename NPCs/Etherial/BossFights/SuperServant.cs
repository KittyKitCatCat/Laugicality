using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Laugicality.NPCs.Etherial.BossFights
{
    public class SuperServant : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int _delay = 0;
        int _index = 0;
        Vector2 _targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 10f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float _theta = 0;
        int _targetType = 0;

        public override void SetDefaults()
        {
            _targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            _index = 0;
            _delay = 0;
            LaugicalityVars.etherial.Add(npc.type);
            npc.width = 36;
            npc.height = 36;
            npc.damage = 40;
            npc.defense = 80;
            npc.lifeMax = 4000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 5;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }
        
        public override void AI()
        {
            MovementType(npc);
        }
        
        private void MovementType(NPC npc)
        {
            _delay++;
            if (_delay > 480)
            {
                _delay = Main.rand.Next(0, 120);
                MirrorTeleport(npc, false);
            }
        }

        
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EtherialEssence"), 1);
        }

        private void MirrorTeleport(NPC npc, bool burst)
        {
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/EtherialChange"));
            if (burst && Main.player[npc.target].statLife > 1)
            {
                for (int i = 0; i < 8; i++)
                {

                    if (Main.netMode != 1)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("EtherialSpiralShot"));
                        Main.npc[n].ai[0] = npc.whoAmI;
                        Main.npc[n].ai[1] = i;
                    }
                }
            }
            npc.position.X = Main.player[npc.target].position.X - (npc.position.X - Main.player[npc.target].position.X);
            npc.position.Y = Main.player[npc.target].position.Y - (npc.position.Y - Main.player[npc.target].position.Y);
            npc.velocity.X = -npc.velocity.X;
            npc.velocity.Y = -npc.velocity.Y;
        }
    }
}
