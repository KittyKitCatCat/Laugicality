using Terraria.ModLoader;

namespace Laugicality.Projectiles
{
	public class DaggorShard : ModProjectile
	{
        public int delay = 0;
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daggor Shard");
            //ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.MinionShot[projectile.type] = true;
		}

		public override void SetDefaults()
		{
            delay = 0;
			projectile.width = 16;
			projectile.height = 16;
			//projectile.alpha = 255;
            projectile.timeLeft = 120;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.minion = true;
        }

        public override void AI()
        {
            projectile.velocity.Y += .1f;
        }
        
    }
}