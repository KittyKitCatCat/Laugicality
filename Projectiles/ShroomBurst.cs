using Terraria;
using Terraria.ModLoader;

namespace Laugicality.Projectiles
{
	public class ShroomBurst : ModProjectile
	{
        public int dir = 0;
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroom Burst");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
		{
            dir = 0;
			projectile.width = 12;
			projectile.height = 12;
			//projectile.alpha = 255;
            projectile.timeLeft = 360;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = 2;
        }
        public override void AI()
        {
            if(dir == 0)
            {
                if (projectile.velocity.X < 0)
                    dir = -1;
                else
                    dir = 1;
            }
            if (Main.rand.Next(4) == 0) Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("Shroom"), projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            if (projectile.velocity.Y < 2)
                projectile.velocity.Y += .1f;
            if(dir == 1)
            {
                if(Main.rand.Next(145) == 0)
                    dir = -1;
                if (projectile.velocity.X < 2)
                    projectile.velocity.X += .1f;
            }
            if (dir == -1)
            {
                if (Main.rand.Next(145) == 0)
                    dir = 1;
                if (projectile.velocity.X > -2)
                    projectile.velocity.X -= .1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //NPCs.Slybertron.Slybertron.electroShockHits += 1;
            int debuff = mod.BuffType("Spored");
            if (debuff >= 0)
            {
                target.AddBuff(debuff, 90, true);
            }      
        }
    }
}