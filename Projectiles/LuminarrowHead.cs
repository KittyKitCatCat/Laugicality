using Terraria.ModLoader;
using System;

namespace Laugicality.Projectiles
{
	public class LuminarrowHead : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Obsidium Arrow Head");     
		}

		public override void SetDefaults()
		{
			projectile.width = 16;               
			projectile.height = 16;              
			projectile.aiStyle = -1;             
			projectile.friendly = true;         
			projectile.hostile = false;         
			projectile.ranged = true;           
			projectile.penetrate = -1;           
			projectile.timeLeft = 240;            
			projectile.ignoreWater = true;         
			projectile.tileCollide = false;          
		}

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}
