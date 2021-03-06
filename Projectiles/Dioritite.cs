using Terraria;
using Terraria.ModLoader;

namespace Laugicality.Projectiles
{
	public class Dioritite : ModProjectile
    {
        public bool bitherial = true;
        public bool stopped = false;
        public int power = 0;
        public int damage = 0;
        public int delay = 0;

        public override void SetDefaults()
        {
            LaugicalityVars.eProjectiles.Add(projectile.type);
            projectile.width = 18;
            projectile.height = 60;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.timeLeft = 60;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.magic = true;
        }

        public override void AI()
        {
            bitherial = true;
            projectile.velocity.Y += .2f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 10);
        }
    }
}