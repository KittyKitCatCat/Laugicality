using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Laugicality.Projectiles
{
    public class AnDiuriken : ModProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0; //Target Velocity
        public float vMag = 0;

        public override void SetDefaults()
        {
            vMax = 20;
            vAccel = .2f;
            reverse = false;
            delay = 100;
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 0;
            projectile.timeLeft = 600;
        }
        
        public override void AI()
        {
            projectile.rotation += 1.57f / 6;
            projectile.velocity.Y += .1f;
            delay -= 1;
            if(delay <= 0 && reverse == false)
            {
                reverse = true;
            }
            if (reverse)
            {
                projectile.tileCollide = false;
                Vector2 delta = Main.player[projectile.owner].Center - projectile.Center;
                float dist = Vector2.Distance(Main.player[projectile.owner].Center, projectile.Center);
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                }
                /*
                if (vMag > tVel)
                {
                    vMag -= vAccel;
                }*/

                if (dist != 0)
                {
                    projectile.velocity = projectile.DirectionTo(Main.player[projectile.owner].Center) * vMag;
                }
                //Return
                if (Math.Abs(delta.X) < 16 && Math.Abs(delta.Y) < 16)
                    projectile.Kill();
            }
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //projectile.ai[0] += 0.1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        /*
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Steamy"), 120);       //Add Onfire buff to the NPC for 1 second
        }*/
    }
}