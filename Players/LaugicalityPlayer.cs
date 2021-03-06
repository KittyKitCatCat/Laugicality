using System;
using System.Collections.Generic;
using System.IO;
using Laugicality.Buffs;
using Laugicality.Extensions;
using Laugicality.Focuses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using Laugicality.NPCs;
using Laugicality.SoulStones;

namespace Laugicality
{
    public sealed partial class LaugicalityPlayer : ModPlayer
    {
        //Potion Gems
        public bool inf = true;
        public bool calm = true;
        public bool ww = true;
        public bool battle = true;
        public bool hunter = true;
        public bool spelunker = true;
        public bool owl = true;
        public bool danger = true;
        public bool feather = true;

        //Steam vars
        public float steamDamage = 1f;

        //Etherial
        public bool etherial;
        public int etherialTrail;
        public int ethBkg;
        public bool etherialSlot;

        //Misc
        public bool zImmune;
        public bool zCool;
        public int zaWarudoDuration;
        public float xTemp;
        public float yTemp;
        public bool zProjImmune;
        public int zCoolDown = 1800;
        public float theta;
        public bool obsHeart;
        public bool crysMag;
        public bool frostbite;
        public int fullBysmal;
        bool _boosted = false;
        float _ringBoost;
        float _fanBoost;
        public float SnowDamage { get; set; } = 1f;
        public bool BysmalAbsorbDisabled { get; set; } = false;

        //Music
        public bool zoneObsidium;
        public bool etherialMusic;

        //Camera Effects
        public int shakeDur;
        public float shakeMag;
        public Vector2 shakeO;
        public bool shakeReset;

        public override void SetupStartInventory(IList<Item> items)
        {
            MysticBurstDisabled = false;
            BysmalAbsorbDisabled = false;
            inf = true;
            calm = true;
            ww = true;
            battle = true;
            hunter = true;
            spelunker = true;
            owl = true;
            danger = true;
            feather = true;
            int[] bysmalItems = { 0, 0, 0 };
        }

        /// <summary>
        /// Challenge : Refactor This to be short and without having it look disgusting
        /// </summary>
        public override void ResetEffects()
        {
            MysticReset();
            ResetSoulStoneEffects();

            if (fullBysmal > 0)
                fullBysmal -= 1; 

            if (shakeDur > 0)
            {
                shakeDur--;
                shakeReset = false;
            }
            else
            {
                shakeMag = 0;

                if (shakeReset == true)
                    shakeO = player.position;

                else
                {
                    player.position = shakeO;
                    shakeReset = true;
                }
            }
            if (Verdi > 0)
                Verdi -= 1;

            Slimey = false;
            Magmatic = false;
            crysMag = false;
            theta += 3.14f / 40f;
            UltraBoisSummon = false;
            obsHeart = false;
            zCoolDown = 65 * 60;
            zaWarudoDuration = 4 * 60;
            Midnight = false;
            AndioChestplate = false;
            AndioChestguard = false;
            ShroomCopterSummon = false;
            zProjImmune = false;
            RockTwinsSummon = false;
            Connected = 0;
            HalfDef = false;
            NoRegen = false;
            TrueCurse = false;
            zImmune = false;
            zCool = false;
            etherialMusic = false;
            Rocks = false;
            Sandy = false;
            Frost = false;
            Obsidium = false;
            Frosty = false;
            Frigid = false;
            MoltenCoreSummon = false;
            SandSharkSummon = false;
            SkeletonPrime = false;
            Doucheron = false;
            TVSummon = false;
            DartCopterSummon = false;
            Electrified = false;
            Steamified = false;
            Mystified = false;
            ToyTrain = false;
            BloodRage = false;
            QueenBee = false;
            Eyes = false;
            Spores = false;
            frostbite = false;
            ArcticHydraSummon = false;

            if (player.extraAccessory)
            {
                player.extraAccessorySlots = 1;

                if (etherialSlot)
                    player.extraAccessorySlots = 2;
            }
            else if (etherialSlot)
                player.extraAccessorySlots = 2;

            if (!player.extraAccessory && !etherialSlot)
                player.extraAccessorySlots = 0;

            SnowDamage = 1f;
            ResetEtherial();

        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (junk)
            {
                caughtType = ItemID.Obsidian;
                return;
            }

            if (zoneObsidium && liquidType == 1 && bait.type == mod.ItemType("LavaGem") && fishingRod.type == ItemID.HotlineFishingHook)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int rand = Main.rand.Next(6);

                    switch (rand)
                    {
                        case 0:
                            caughtType = ItemID.Topaz;
                            break;
                        case 1:
                            caughtType = ItemID.Amethyst;
                            break;
                        case 2:
                            caughtType = ItemID.Sapphire;
                            break;
                        case 3:
                            caughtType = ItemID.Emerald;
                            break;
                        case 4:
                            caughtType = ItemID.Ruby;
                            break;
                        default:
                            caughtType = ItemID.Diamond;
                            break;
                    }
                }

                if (NPC.downedBoss2)
                {
                    if(Main.rand.Next(3) == 0)
                        caughtType = mod.ItemType("ObsidiumOre");

                    if (Main.rand.Next(4) == 0)
                        caughtType = mod.ItemType("ObsidiumBar");
                }

                if (LaugicalityWorld.downedRagnar)
                {
                    if (Main.rand.Next(5) == 0)
                        caughtType = mod.ItemType("MagmaSnapper");

                    else if (Main.rand.Next(4) == 0)
                        caughtType = mod.ItemType("ObsidiumChunk");
                }

                if (Main.rand.Next(5) == 0)
                    caughtType = ItemID.Obsidian;

                if (Main.rand.Next(25) == 0)
                {
                    int rand = Main.rand.Next(6);

                    switch (rand)
                    {
                        case 0:
                            caughtType = ItemID.LavaCharm;
                            break;
                        case 1:
                            caughtType = mod.ItemType("ObsidiumLily");
                            break;
                        case 2:
                            caughtType = mod.ItemType("FireDust");
                            break;
                        case 3:
                            caughtType = mod.ItemType("Eruption");
                            break;
                        case 4:
                            caughtType = mod.ItemType("CrystalizedMagma");
                            break;
                        default:
                            caughtType = mod.ItemType("MagmaHeart");
                            break;
                    }
                }
            }
        }

        public override void PreUpdate()
        {
            etherial = LaugicalityWorld.downedEtheria;
            Random random = new Random();

            if (shakeDur > 0)
            {
                shakeMag += 1f / 5f;
                player.position.X = shakeO.X - shakeMag + (float)random.NextDouble() * shakeMag * 2;
                player.position.Y = shakeO.Y - shakeMag + (float)random.NextDouble() * shakeMag * 2;
            }

            CheckBysmalPowers();
            
            if (LaugicalityWorld.downedEtheria || Etherable > 0)
                GetEtherialAccessories();
        }

        public override void PostUpdate()
        {
            PostUpdateZaWarudo();
            PostUpdateMysticBursts();
            PostUpdateMysticBuffs();
            PostUpdateMovementTileChecks();
        }

        private void PostUpdateZaWarudo()
        {
            if (Laugicality.zaWarudo > 0 && zImmune == false)
            {
                player.velocity.X = 0;
                player.velocity.Y = 0;
                player.AddBuff(mod.BuffType("TrueCurse"), 1, true);

                if (xTemp == 0 || yTemp == 0)
                {
                    xTemp = player.position.X;
                    yTemp = player.position.Y;
                }
                else
                {
                    player.position.X = xTemp;
                    player.position.Y = yTemp;
                }
            }
            else if (Frosty)
            {
                if (xTemp == 0 || yTemp == 0)
                {
                    xTemp = player.position.X;
                    yTemp = player.position.Y;
                }
                else
                {
                    player.position.X = xTemp;
                    player.position.Y = yTemp;
                }
            }
            else
            {
                xTemp = 0;
                yTemp = 0;
            }
        }
        

        private void PostAccessories()
        {
            if (Verdi > 0)
                player.maxRunSpeed += .1f;
        }

        private void PostUpdateMysticBursts()
        {
            if (MysticErupting > 0)
            {
                if (Main.rand.Next(4) == 0)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, player.velocity.X - 4 + Main.rand.Next(9), -Main.rand.Next(6, 9), mod.ProjectileType("Eruption"), (int)(30 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
            }

            if (MysticSpiralBurst > 0)
            {
                MysticSpiralDelay++;

                if (MysticSpiralDelay > 2)
                {
                    MysticSpiralDelay = 0;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 4 * (float)Math.Cos(theta * 2), 4 * (float)Math.Sin(theta * 2), mod.ProjectileType("AnDioChestguardBurst"), (int)(32 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                }
            }

            if (MysticSteamSpiralBurst > 0)
            {
                MysticSteamSpiralDelay++;

                if (MysticSteamSpiralDelay > 5)
                {
                    MysticSteamSpiralDelay = 0;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 * (float)Math.Cos(theta), 6 * (float)Math.Sin(theta), mod.ProjectileType("SteamBurst"), (int)(40 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 * (float)Math.Cos(theta + 3.14f), 6 * (float)Math.Sin(theta + 3.14f), mod.ProjectileType("SteamBurst"), (int)(40 * MysticDamage * MysticBurstDamage), 3, Main.myPlayer);
                }
            }
        }

        private void PostUpdateMovementTileChecks()
        {
            CheckVent();
            CheckRing();
            CheckFan();
            CheckFanRight();
        }

        private void CheckVent()
        {
            if (Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].type == mod.TileType("SteamVENT"))
            {
                if(player.velocity.Y >= 0)
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/BrassFAN"));

                player.velocity.Y = -25;
            }
        }

        private void CheckRing()
        {
            float vSpeed = player.velocity.Y;
            float minVSpeed = 10;
            float maxVSpeed = 50;

            if (Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].type == mod.TileType("BrassRING") && vSpeed < 0)
            {
                if (_ringBoost == 0 && Math.Abs(player.velocity.Y) > 1)
                {
                    if (vSpeed > -minVSpeed)
                        player.velocity.Y = -minVSpeed;

                    _ringBoost = player.velocity.Y * 2f;
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/BrassRING"));

                    if (_ringBoost < -maxVSpeed)
                        _ringBoost = -maxVSpeed;
                }

                if (Math.Abs(_ringBoost) > 1)
                    player.velocity.Y = _ringBoost;
            }
            else
                _ringBoost = 0;
        }

        private void CheckFan()
        {
            float hSpeed = player.velocity.X;
            float minHSpeed = 10;
            float maxHSpeed = 50;

            if (Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].type == mod.TileType("BrassFAN"))
            {
                if (_fanBoost == 0)
                {
                    if (hSpeed > -minHSpeed)
                        player.velocity.X = -minHSpeed;

                    _fanBoost = player.velocity.X * 2f;
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/BrassFAN"));

                    if (_fanBoost < -maxHSpeed)
                        _fanBoost = -maxHSpeed;
                }

                if (Math.Abs(_fanBoost) > 1)
                    player.velocity.X = _fanBoost;
            }
            else
                _fanBoost = 0;
        }
        
        private void CheckFanRight()
        {
            float hSpeed = player.velocity.X;
            float minHSpeed = 10;
            float maxHSpeed = 50;

            if (Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].type == mod.TileType("BrassFANRight"))
            {
                if (_fanBoost == 0)
                {
                    if (hSpeed < minHSpeed)
                        player.velocity.X = minHSpeed;

                    _fanBoost = player.velocity.X * 2f;
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/BrassFAN"));

                    if (_fanBoost > maxHSpeed)
                        _fanBoost = maxHSpeed;
                }

                if (Math.Abs(_fanBoost) > 1)
                    player.velocity.X = _fanBoost;
            }
            else
                _fanBoost = 0;
        }

        /*
        private void CheckRepCore()
        {
            if (Main.tile[(int)(player.Center.X / 16 + player.velocity.X / 16), (int)(player.Center.Y / 16)].type == mod.TileType("RepulsionCore") && Math.Abs(player.velocity.X) > 4)
            {
                player.velocity.X = -player.velocity.X;
            }
            if (Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16 + player.velocity.Y / 16)].type == mod.TileType("RepulsionCore") && Math.Abs(player.velocity.Y) > 4)
            {
                player.velocity.Y = -player.velocity.Y;
            }
        }*/

        public override void PostUpdateEquips()
        {
            CheckBysmalPowers();
            PostAccessories();
            if (Verdi > 0)
            {
                player.maxRunSpeed *= 1.1f;
            }
            if (LaugicalityWorld.downedEtheria || Etherable > 0)
                GetEtherialAccessoriesPost();
        }

        public override void GetWeaponDamage(Item item, ref int damage)
        {
            if (item.ammo == AmmoID.Snowball)
                damage = (int)((float)damage * SnowDamage);
            base.GetWeaponDamage(item, ref damage);
        }

        public override TagCompound Save()
        {
            return new TagCompound {
                {"Class", Class },
                {"Etherial", etherial },
                {"ESlot", etherialSlot },
                {"SoulStoneMove", SoulStoneMovement },
                {"SoulStoneVis", SoulStoneVisuals },
                {"Inferno", inf},
                {"Calming", calm},
                {"WaterWalking", ww},
                {"Battle", battle},
                {"Hunter", hunter},
                {"Spelunker", spelunker},
                {"NightOwl", owl},
                {"Dangersense", danger},
                {"Featherfall", feather},
                {"BysmalPowers", BysmalPowers},
                {"LuxMaxPermaBoost", LuxMaxPermaBoost},
                {"VisMaxPermaBoost", VisMaxPermaBoost},
                {"MundusMaxPermaBoost", MundusMaxPermaBoost},
                {"MysticBurstDisabled", MysticBurstDisabled},
                {"BysmalAbsorbDisabled", BysmalAbsorbDisabled},
            };
        }

        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("Laugicality:Etherial", LaugicalityWorld.downedEtheria);
            player.ManageSpecialBiomeVisuals("Laugicality:Etherial2", !Main.dayTime && LaugicalityWorld.downedEtheria);

            player.ManageSpecialBiomeVisuals("Laugicality:ZaWarudo", Laugicality.zaWarudo > 0);
        }

        public override void Load(TagCompound tag)
        {
            Class = tag.GetInt("Class");
            etherial = tag.GetBool("Etherial");
            etherialSlot = tag.GetBool("ESlot");
            SoulStoneMovement = tag.GetBool("SoulStoneMove");
            SoulStoneVisuals = tag.GetBool("SoulStoneVis");
            inf = tag.GetBool("Inferno");
            calm = tag.GetBool("Calming");
            ww = tag.GetBool("WaterWalking");
            battle = tag.GetBool("Battle");
            hunter = tag.GetBool("Hunter");
            spelunker = tag.GetBool("Spelunker");
            owl = tag.GetBool("NightOwl");
            danger = tag.GetBool("Dangersense");
            feather = tag.GetBool("Featherfall");
            LuxMaxPermaBoost = tag.GetFloat("LuxMaxPermaBoost");
            VisMaxPermaBoost = tag.GetFloat("VisMaxPermaBoost");
            MundusMaxPermaBoost = tag.GetFloat("MundusMaxPermaBoost");
            MysticBurstDisabled = tag.GetBool("MysticBurstDisabled");
            BysmalAbsorbDisabled = tag.GetBool("BysmalAbsorbDisabled");
            BysmalPowers = (List<int>)tag.GetList<int>("BysmalPowers");
        }
        
        public override void UpdateBiomes()
        {
            zoneObsidium = (LaugicalityWorld.obsidiumTiles > 150 && player.position.Y > WorldGen.rockLayer + 150);
            etherialMusic = etherial;
        }
        
        public override bool CustomBiomesMatch(Player other)
        {
            LaugicalityPlayer modOther = other.GetModPlayer<LaugicalityPlayer>(mod);
            return zoneObsidium == modOther.zoneObsidium;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            LaugicalityPlayer modOther = other.GetModPlayer<LaugicalityPlayer>(mod);
            modOther.zoneObsidium = zoneObsidium;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = zoneObsidium;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            zoneObsidium = flags[0];
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (zoneObsidium)
                return mod.GetTexture("ObsidiumBiomeMapBackground");

            return null;
        }

        public override void UpdateDead()
        {
            Electrified = false;
        }

        /// <summary>
        /// Refactor This to be short
        /// </summary>
        public override void UpdateBadLifeRegen()
        {
            
            //Main.NewText(mysticDuration.ToString(), 250, 250, 0);
            if (HalfDef)
            {
                player.statDefense /= 2;
            }

            if (NoRegen)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen = -1;
            }

            if (Electrified)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 16;
            }

            if (frostbite)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 32;
            }

            if (Mystified)//Mystified
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 4;
            }
        }

        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();

            UpdateSoulStoneLifeRegen();
        }


        public override bool PreItemCheck()
        {
            if (TrueCurse)
                return false;

            return true;
        }

        /// <summary>
        /// TODO Refactor This to be short
        /// </summary>
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            int rand = Main.rand.Next(4);

            if (Obsidium)
                target.AddBuff(BuffID.OnFire, (int)((120 + 60 * rand)), false);

            if (Frost)
                target.AddBuff(BuffID.Frostburn, (int)((120 + 60 * rand)), false);

            if (SkeletonPrime)
                target.AddBuff(39, (int)((120 + 60 * rand)), false);

            if (Doucheron)
                target.AddBuff(70, (int)((120 + 60 * rand)), false);

            if (QueenBee)
                target.AddBuff(20, (int)((120 + 60 * rand)), false);

            if (Steamified)
                target.AddBuff(mod.BuffType("Steamy"), (int)((120 + 60 * rand)), false);

            if (Slimey)
                target.AddBuff(mod.BuffType("Slimed"), (int)((120 + 60 * rand)), false);

            if (EtherialFrost)
                target.AddBuff(mod.BuffType("Frostbite"), (int)((12 * 60 + 60 * rand)), false);

            if (EtherialPipes)
                target.AddBuff(mod.BuffType("Steamified"), (int)((12 * 60 + 60 * rand)), false);

            if (crysMag)
            {
                if (crit)
                {
                    float mag = 6f;
                    float theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);

                    if (Main.netMode != 1)
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, mod.ProjectileType("ObsidiumArrowHead"), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);

                    if (Main.netMode != 1)
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, mod.ProjectileType("ObsidiumArrowHead"), damage, 3f, Main.myPlayer);
                    theta2 = (float)(Main.rand.NextDouble() * 2 * Math.PI);

                    if (Main.netMode != 1)
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)Math.Cos(theta2) * mag, (float)Math.Sin(theta2) * mag, mod.ProjectileType("ObsidiumArrowHead"), damage, 3f, Main.myPlayer);
                }
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

            if (etherialTrail > 0)
            {
                DrawEtherialTrailEffect();
            }

            if (Electrified)
            {
                DrawSteamEffect(drawInfo, ref r, ref g, ref b, out fullBright);
            }

            if (etherial)
            {
                DrawEtherialEffect(out r, out g, out b);
            }

            if(EtherialTank)
            {
                DrawEtherialTankSteam();
            }

            if(MysticHold > 0)
            {
                //DrawMysticUI();
            }
        }

        private void DrawEtherialTrailEffect()
        {
            etherialTrail -= 1;

            if (Main.rand.Next(0, 4) == 0)
            {
                Dust.NewDust(player.position + player.velocity, player.width, player.height, mod.DustType("Etherial"), 0f, 0f);
            }
        }

        private void DrawSteamEffect(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, out bool fullBright)
        {
            if (Main.rand.Next(13) == 0 && drawInfo.shadow == 0f)
            {
                int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4,
                    mod.DustType("TrainSteam"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                Main.dust[dust].noGravity = false;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
                Main.playerDrawDust.Add(dust);
            }

            r *= 0.1f;
            g *= 0.8f;
            b *= 0.8f;
            fullBright = true;
        }

        private void DrawEtherialEffect(out float r, out float g, out float b)
        {
            r = 0.2f;
            g = 0.9f;
            b = 1f;

            if (ethBkg <= 0)
            {
                ethBkg = 9;
            }
            else
            {
                ethBkg -= 1;
            }
        }

        private void DrawEtherialTankSteam()
        {
            if (Math.Abs(player.velocity.X) > 14f && SoulStoneVisuals)
            {
                Rectangle rect = player.getRect();
                Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, 0, mod.DustType("TrainSteam"));
            }
        }

        //Hotkey
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Laugicality.toggleMystic.JustPressed && MysticHold > 0)
            {
                MysticSwitch();
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/MysticSwitch"));
            }

            if (Laugicality.quickMystica.JustPressed && Mysticality == 0)
            {
                bool mysticaPotion = false;

                foreach (Item item in player.inventory)
                {
                    if (item.type == mod.ItemType<SupremeMysticaPotion>())
                    {
                        mysticaPotion = true;
                        item.stack -= 1;
                        Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 3);
                        if (Lux < (LuxMax + LuxMaxPermaBoost) * (1 + (LuxOverflow - 1) / 2))
                            Lux = (LuxMax + LuxMaxPermaBoost) * (1 + (LuxOverflow - 1) / 2);

                        if (Vis < (VisMax + VisMaxPermaBoost) * (1 + (VisOverflow - 1) / 2))
                            Vis = (VisMax + VisMaxPermaBoost) * (1 + (VisOverflow - 1) / 2);

                        if (Mundus < (MundusMax + MundusMaxPermaBoost) * (1 + (MundusOverflow - 1) / 2))
                            Mundus = (MundusMax + MundusMaxPermaBoost) * (1 + (MundusOverflow - 1) / 2);

                        player.AddBuff(mod.BuffType<Mysticality3>(), 60 * Constants.TICKS_PER_SECONDS, true);
                    }

                    if (mysticaPotion)
                        break;
                }

                if(!mysticaPotion)
                {
                    foreach (Item item in player.inventory)
                    {
                        if (item.type == mod.ItemType<GreaterMysticaPotion>())
                        {
                            mysticaPotion = true;
                            item.stack -= 1;

                            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 3);

                            if (Lux < LuxMax + LuxMaxPermaBoost)
                                Lux = LuxMax + LuxMaxPermaBoost;

                            if (Vis < VisMax + VisMaxPermaBoost)
                                Vis = VisMax + VisMaxPermaBoost;

                            if (Mundus < MundusMax + MundusMaxPermaBoost)
                                Mundus = MundusMax + MundusMaxPermaBoost;

                            player.AddBuff(mod.BuffType<Mysticality2>(), 60 * Constants.TICKS_PER_SECONDS, true);
                        }

                        if (mysticaPotion)
                            break;
                    }
                }

                if (!mysticaPotion)
                {
                    foreach (Item item in player.inventory)
                    {
                        if (item.type == mod.ItemType("MysticaPotion"))
                        {
                            mysticaPotion = true;
                            item.stack -= 1;
                            Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 3);
                            Lux = LuxMax + LuxMaxPermaBoost;
                            Vis = VisMax + VisMaxPermaBoost;
                            Mundus = MundusMax + MundusMaxPermaBoost;
                            player.AddBuff(mod.BuffType("Mysticality"), 60 * 60, true);
                        }

                        if (mysticaPotion)
                            break;
                    }
                }
            }

            if (Laugicality.toggleSoulStoneV.JustPressed)
            {
                SoulStoneVisuals = !SoulStoneVisuals;
                Main.NewText("Soul Stone and Potion Crystal visual effects: " + SoulStoneVisuals.ToString(), 250, 250, 0);
            }

            if (Laugicality.toggleSoulStoneM.JustPressed)
            {
                SoulStoneMovement = !SoulStoneMovement;
                Main.NewText("Soul Stone and Potion Crystal mobility effects: " + SoulStoneMovement.ToString(), 250, 250, 0);
            }
        }


        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (MysticSwitchCool <= 0)
            {
                if (BloodRage)
                    ApplyBloodRage();

                SpawnProjectileOnPlayerHurt();
                ArmorEffectPlayerHurt();
                MysticSwitchCool = 120;
            }
        }

        private void ArmorEffectPlayerHurt()
        {
            if (AndioChestguard && player.statLife < player.statLifeMax2 / 4 && zCool == false)
                ZaWarudo();

            if (AndioChestplate && player.statLife < player.statLifeMax2 / 4 && zCool == false)
                ZaWarudo();
        }
        

        private void ZaWarudo()
        {
            Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/zaWarudo"));
            player.AddBuff(mod.BuffType("TimeExhausted"), zCoolDown, true);

            if(Laugicality.zaWarudo < zaWarudoDuration)
            {
                Laugicality.zaWarudo = zaWarudoDuration;
                LaugicalGlobalNPCs.zTime = zaWarudoDuration;
            }
        }


        private void SpawnProjectileOnPlayerHurt()
        {
            if (Eyes)
                SpawnMiniEye();

            if (Sandy)
                SpawnSandBall();

            if (Frigid)
                SpawnIceShard();

            if (Spores)
                SpawnSpore();

            if (Rocks)
                SpawnRockShard();
        }

        private void ApplyBloodRage()
        {
            player.AddBuff(mod.BuffType("BloodRage"), 420);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 0, 565, 16, 3f, player.whoAmI);
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 0, 565, 16, 3f, player.whoAmI);
        }

        private void SpawnMiniEye()
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);

            if (Main.rand.Next(0, 2) == 0)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                    mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);

            if (Main.rand.Next(0, 2) == 0)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                    mod.ProjectileType("MiniEye"), 16, 3f, player.whoAmI);
        }

        private void SpawnSandBall()
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 4, 4, mod.ProjectileType("Sandball"), 18, 5,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 4, -4, mod.ProjectileType("Sandball"), 18, 5,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -4, -4, mod.ProjectileType("Sandball"), 18, 5,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -4, 4, mod.ProjectileType("Sandball"), 18, 5,
                Main.myPlayer);
        }

        private void SpawnIceShard()
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("IceShardF"), 16, 3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12),
                mod.ProjectileType("IceShardF"), 16, 3f, player.whoAmI);
        }

        private void SpawnSpore()
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 567, 48,
                3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 568, 48,
                3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 569, 48,
                3f, player.whoAmI);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 570, 48,
                3f, player.whoAmI);
            if (Main.rand.Next(0, 2) == 0)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 571,
                    48, 3f, player.whoAmI);

            if (Main.rand.Next(0, 2) == 0)
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 6 - Main.rand.Next(12), 6 - Main.rand.Next(12), 567,
                    48, 3f, player.whoAmI);
        }

        private void SpawnRockShard()
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 8, 0, mod.ProjectileType("RockShard"), 20, 3,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8, 0, mod.ProjectileType("RockShard"), 20, 3,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 8, mod.ProjectileType("RockShard"), 20, 3,
                Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17),
                mod.ProjectileType("RockShard"), 20, 3, Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17),
                mod.ProjectileType("RockShard"), 20, 3, Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17),
                mod.ProjectileType("RockShard"), 20, 3, Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17),
                mod.ProjectileType("RockShard"), 20, 3, Main.myPlayer);

            Projectile.NewProjectile(player.Center.X, player.Center.Y, -8 + Main.rand.Next(0, 17), -8 + Main.rand.Next(0, 17),
                mod.ProjectileType("RockShard"), 20, 3, Main.myPlayer);
        }


        #region Buffs

        public const int MAX_BUFFS = 42;

        public bool Obsidium { get; set; }

        public bool Frost { get; set; }

        public bool Frigid { get; set; }

        public bool Frosty { get; set; }

        public bool Rocks { get; set; }

        public bool Sandy { get; set; }

        public bool TrueCurse { get; set; }

        public bool NoRegen { get; set; }

        public bool HalfDef { get; set; }

        public int Connected { get; set; }

        public int Verdi { get; set; }

        #endregion

        #region Summons
        public bool MoltenCoreSummon { get; set; }

        public bool TVSummon { get; set; }

        public bool SandSharkSummon { get; set; }

        public bool DartCopterSummon { get; set; }

        public bool RockTwinsSummon { get; set; }

        public bool ShroomCopterSummon { get; set; }

        public bool UltraBoisSummon { get; set; }

        public bool ArcticHydraSummon { get; set; }

        #endregion

        // TODO Change this to a class.
        #region Soul Stone

        public Focus Focus => FocusManager.Instance.Vitality;
        public int Class { get; set; }

        public bool SoulStoneVisuals { get; set; } = true;

        public bool SoulStoneMovement { get; set; } = true;

        public bool SkeletonPrime { get; set; }

        public bool Doucheron { get; set; }

        public bool Electrified { get; set; }

        public bool Steamified { get; set; }

        public bool Mystified { get; set; }

        public bool ToyTrain { get; set; }

        public bool BloodRage { get; set; }

        public bool QueenBee { get; set; }

        public bool Eyes { get; set; }

        public bool Spores { get; set; }

        public bool Slimey { get; set; }

        #endregion // TODO Verify if name matches.
    }
}

