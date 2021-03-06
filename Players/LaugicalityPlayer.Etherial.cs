﻿using System;
using System.Collections.Generic;
using Laugicality.Buffs;
using Laugicality.NPCs.Bosses;
using Laugicality.NPCs.PreTrio;
using Laugicality.NPCs.RockTwins;
using Laugicality.NPCs.Slybertron;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;

namespace Laugicality
{
    public sealed partial class LaugicalityPlayer
    {
        public void CycleBysmalPowers(int newPower)
        {
            if(!BysmalPowers.Contains(newPower))
            {
                if (BysmalPowers.Count > 2)
                    BysmalPowers.RemoveAt(0);

                BysmalPowers.Add(newPower);
            }
        }
        
        private void ResetEtherial()
        {
            AnnihilationBoost = false;
            JusticeCooldown = false;
            EtherialGel = false;
            EtherVision = false;
            EtherialScarf = false;
            EtherialScarfCooldown = false;
            EtherialBrain = false;
            EtherialBrainCooldown = false;
            EtherialFrost = false;
            EtherialBees = false;
            EtherialMagma = false;
            EtherialBones = false;
            EtherBonesBoost = false;
            EtherialAnDio = false;
            EtherialTwins = false;
            EtherialDestroyer = false;
            EtherialPrime = false;
            EtherCog = false;
            EtherialPipes = false;
            EtherialTank = false;
            EtherialSpores = false;
            EtherialStone = false;
            EtherialTruffle = false;

            if(Etherable > 0)
                Etherable -= 1;
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            CheckBysmalPowers();

            if (EtherialBrain && !EtherialBrainCooldown && (LaugicalityWorld.downedEtheria || Etherable > 0))
            {
                npc.AddBuff(mod.BuffType("FragmentedMind"), 15 * 60, false);

                if (damage >= player.statLife)
                {
                    if (EtherialBrain && !EtherialBrainCooldown && (LaugicalityWorld.downedEtheria || Etherable > 0))
                    {
                        player.AddBuff(mod.BuffType("FragmentedMind"), 60 * 60 * 3, true);
                        player.immune = true;
                        player.immuneTime = 2 * 60;
                        player.statLife += 300;

                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/EtherialChange"));

                        for (int i = 0; i < 20; i++)
                            Dust.NewDust(player.position + player.velocity, player.width, player.height, mod.DustType("Etherial"), 0f, 0f);
                    }
                }
            }

            if(EtherialTank)
            {
                npc.life -= (int)(Math.Abs(player.velocity.X) * 500);

                if (npc.life <= 0)
                    npc.life = 1;
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            CheckBysmalPowers();

            if (!SoulStonePreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource))
                return false;

            if (EtherialBones)
            {
                player.AddBuff(mod.BuffType<EtherBones>(), 10 * 60, true);
                EtherBonesDamageBoost += ((float)damage / (float)player.statLifeMax2);
            }

            if(EtherialTwins && !JusticeCooldown)
            {
                player.AddBuff(mod.BuffType("JusticeCooldown"), 90 * 60, true);
                player.statLife += damage;
                player.immune = true;
                player.immuneTime = 1 * 60;

                return false;
            }

            if (damage >= player.statLife)
            {
                if (EtherialScarf && !EtherialScarfCooldown && (LaugicalityWorld.downedEtheria || Etherable > 0))
                {
                    player.AddBuff(mod.BuffType("EtherialScarfCooldown"), 60 * 60 * 1, true);
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/EtherialChange"));

                    for (int i = 0; i < 20; i++)
                        Dust.NewDust(player.position + player.velocity, player.width, player.height, mod.DustType("Etherial"), 0f, 0f);

                    player.immune = true;
                    player.immuneTime = 2 * 60;

                    return false;
                }
            }

            return true;
        }

        // TODO Use BysmalPower class.
        private void CheckBysmalPowers()
        {
            if(fullBysmal > 0)
            {
                if (BysmalPowers.Contains(NPCID.KingSlime))
                    EtherialGel = true;

                if (BysmalPowers.Contains(NPCID.EyeofCthulhu))
                    EtherVision = true;

                if (BysmalPowers.Contains(NPCID.EaterofWorldsHead))
                    EtherialScarf = true;

                if (BysmalPowers.Contains(NPCID.BrainofCthulhu))
                    EtherialBrain = true;

                if (BysmalPowers.Contains(mod.NPCType<Hypothema>()))
                    EtherialFrost = true;

                if (BysmalPowers.Contains(NPCID.QueenBee))
                    EtherialBees = true;

                if (BysmalPowers.Contains(mod.NPCType<Ragnar>()))
                    EtherialMagma = true;

                if (BysmalPowers.Contains(NPCID.SkeletronHead))
                    EtherialBones = true;

                if (BysmalPowers.Contains(mod.NPCType<AnDio3>()))
                    EtherialAnDio = true;

                if (BysmalPowers.Contains(NPCID.Retinazer) || BysmalPowers.Contains(NPCID.Spazmatism))
                    EtherialTwins = true;

                if (BysmalPowers.Contains(NPCID.TheDestroyer))
                    EtherialDestroyer = true;

                if (BysmalPowers.Contains(NPCID.SkeletronPrime))
                    EtherialPrime = true;

                if (BysmalPowers.Contains(mod.NPCType<TheAnnihilator>()))
                    EtherCog = true;

                if (BysmalPowers.Contains(mod.NPCType<Slybertron>()))
                    EtherialPipes = true;

                if (BysmalPowers.Contains(mod.NPCType<NPCs.SteamTrain.SteamTrain>()))
                    EtherialTank = true;

                if (BysmalPowers.Contains(NPCID.Plantera))
                    EtherialSpores = true;

                if (BysmalPowers.Contains(NPCID.Golem))
                    EtherialStone = true;

                if (BysmalPowers.Contains(NPCID.DukeFishron))
                    EtherialTruffle = true;

                if (BysmalPowers.Contains(NPCID.MoonLordCore))
                    Etherable = 2;
            }
        }

        private void GetEtherialAccessories()
        {
            LaugicalityPlayer modPlayer = player.GetModPlayer<LaugicalityPlayer>(mod);

            if(EtherialGel)
            {
                player.jumpSpeedBoost += 5.0f;
                player.moveSpeed += .5f;
                player.maxRunSpeed += 3f;
            }

            if (EtherialMagma && player.lavaWet)
                player.AddBuff(mod.BuffType("EtherialRagnar"), 15 * 60);

            if (EtherialAnDio)
                modPlayer.zProjImmune = true;

            if (EtherCog)
                player.maxMinions += 4;

            if(EtherialPipes)
            {
                player.thrownDamage += .30f;
                player.thrownVelocity += .5f;
            }

            if (EtherialTank)
            {
                player.jumpSpeedBoost += 3.0f;
                player.maxRunSpeed += 4f;
                player.moveSpeed += 4f;

            }

            if (EtherialTruffle)
            {
                player.gills = true;
                player.ignoreWater = true;
                player.accFlipper = true;

                if (player.wet)
                {
                    player.jumpSpeedBoost += 3.0f;
                    player.moveSpeed += .5f;
                    player.maxRunSpeed += 3f;
                }
            }
        }


        private void GetEtherialAccessoriesPost()
        {
            LaugicalityPlayer modPlayer = player.GetModPlayer<LaugicalityPlayer>(mod);

            if (EtherialBees && player.honey)
            {
                player.lifeRegen += 8;
                player.statDefense += 15;

                const float dmgBoost = .15f;

                player.thrownDamage += dmgBoost;
                player.rangedDamage += dmgBoost;
                player.magicDamage += dmgBoost;
                player.minionDamage += dmgBoost;
                player.meleeDamage += dmgBoost;
            }

            if (EtherialMagma)
                player.statLifeMax2 = (int)(player.statLifeMax2 * 1.25f);

            if (EtherialBones)
            {
                if(EtherBonesBoost)
                {
                    if (EtherBonesDamageBoost > 1)
                        EtherBonesDamageBoost = 1;

                    player.thrownDamage += EtherBonesDamageBoost;
                    player.rangedDamage += EtherBonesDamageBoost;
                    player.magicDamage += EtherBonesDamageBoost;
                    player.minionDamage += EtherBonesDamageBoost;
                    player.meleeDamage += EtherBonesDamageBoost;
                }
                else
                    EtherBonesDamageBoost = 0;
            }

            if (EtherialAnDio)
                modPlayer.zProjImmune = true;

            if (EtherialDestroyer)
            {
                int originalDef = player.statDefense;

                float globalDmg = player.meleeDamage - 1;

                if (player.rangedDamage - 1 < globalDmg)
                    globalDmg = player.rangedDamage - 1;

                if (player.magicDamage - 1 < globalDmg)
                    globalDmg = player.magicDamage - 1;

                if (player.thrownDamage - 1 < globalDmg)
                    globalDmg = player.thrownDamage - 1;

                if (player.minionDamage - 1 < globalDmg)
                    globalDmg = player.minionDamage - 1;

                if (globalDmg > 0)
                {
                    if (globalDmg > 2)
                        globalDmg = 2;

                    player.statDefense += (int)(originalDef * globalDmg);
                }
            }

            if (EtherialPrime)
            {
                float lifePercentage = (float)(player.statLifeMax2 - player.statLife) / (float)player.statLifeMax2;

                player.thrownDamage += lifePercentage;
                player.rangedDamage += lifePercentage;
                player.magicDamage += lifePercentage;
                player.minionDamage += lifePercentage;
                player.meleeDamage += lifePercentage;
            }

            if (EtherCog)
            {
                if (AnnihilationBoost)
                {
                    if (AnnihilationDamageBoost > 1)
                        AnnihilationDamageBoost = 1;

                    player.thrownDamage += AnnihilationDamageBoost;
                    player.rangedDamage += AnnihilationDamageBoost;
                    player.magicDamage += AnnihilationDamageBoost;
                    player.minionDamage += AnnihilationDamageBoost;
                    player.meleeDamage += AnnihilationDamageBoost;
                }
                else
                    AnnihilationDamageBoost = 0;
            }

            if (EtherialTank)
            {
                float moveSpeed = (float)Math.Abs(player.velocity.X) / 25f;

                player.thrownDamage += moveSpeed;
                player.rangedDamage += moveSpeed;
                player.magicDamage += moveSpeed;
                player.minionDamage += moveSpeed;
                player.meleeDamage += moveSpeed;
            }

            if (EtherialSpores)
            {
                if (player.grapCount > 0)
                {
                    GrappleReturned = false;
                    player.lifeRegen += 15;

                    const float dmgBoost = .5f;

                    player.thrownDamage += dmgBoost;
                    player.rangedDamage += dmgBoost;
                    player.magicDamage += dmgBoost;
                    player.minionDamage += dmgBoost;
                    player.meleeDamage += dmgBoost;
                }
            }

            if (EtherialStone)
            {
                player.lifeRegen += 18;
                player.statDefense += 20;
                player.statLifeMax2 += player.statDefense;
            }

            if (EtherialTruffle)
            {
                if(player.wet)
                {
                    player.lifeRegen += 12;
                    player.statDefense += 35;

                    const float dmgBoost = .4f;

                    player.thrownDamage += dmgBoost;
                    player.rangedDamage += dmgBoost;
                    player.magicDamage += dmgBoost;
                    player.minionDamage += dmgBoost;
                    player.meleeDamage += dmgBoost;
                }
            }
        }
        
        // TODO Change this to BysmalPower class.
        public List<int> BysmalPowers { get; internal set; } = new List<int>();


        #region Etherial Accessories

        public bool EtherialGel { get; set; }

        public bool EtherVision { get; set; }

        public bool EtherialScarf { get; set; }

        public bool EtherialScarfCooldown { get; set; }

        public bool EtherialBrain { get; set; }

        public bool EtherialBrainCooldown { get; set; }

        public bool EtherialFrost { get; set; }

        public bool EtherialBees { get; set; }

        public bool EtherialMagma { get; set; }

        public bool EtherialBones { get; set; }

        public bool EtherialAnDio { get; set; }

        public bool EtherialTwins { get; set; }

        public bool EtherialDestroyer { get; set; }

        public bool EtherialPrime { get; set; }

        public bool EtherCog { get; set; }

        public bool EtherialPipes { get; set; }

        public bool EtherialTank { get; set; }

        public bool EtherialSpores { get; set; }

        public bool EtherialStone { get; set; }

        public bool EtherialTruffle { get; set; }

        public int Etherable { get; set; }

        public float EtherBonesDamageBoost { get; set; }

        public bool EtherBonesBoost { get; set; }

        public bool GrappleReturned { get; set; }

        public bool JusticeCooldown { get; set; }

        public bool AnnihilationBoost { get; set; }

        public float AnnihilationDamageBoost { get; set; }

        #endregion
    }
}
