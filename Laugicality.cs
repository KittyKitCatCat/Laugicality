using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Laugicality.Etherial;
using System.IO;
using Terraria.UI;
using System.Collections.Generic;
using Laugicality.Focuses;
using Laugicality.Items.Consumables;
using Laugicality.Items.Placeable.MusicBoxes;
using Laugicality.UI;
using Terraria.Graphics.Shaders;

namespace Laugicality
{
    public class Laugicality : Mod
    {
        public const string GOLD_BARS_GROUP = "GldBars";        

        internal static ModHotKey toggleMystic, toggleSoulStoneV, toggleSoulStoneM, quickMystica, soulStoneAbility; // ;) ;)

        public static Laugicality instance;
        public static int zaWarudo = 0;

        public Laugicality()
        {

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };

        }

        #region Recipe Groups
        public override void AddRecipeGroups()
        {
            //Emblems
            RecipeGroup.RegisterGroup("Emblems", new RecipeGroup(() => Lang.misc[37] + " Emblem", new int[]
            {
                ItemID.RangerEmblem,
                ItemID.WarriorEmblem,
                ItemID.SorcererEmblem,
                ItemID.SummonerEmblem,
                ItemType("NullEmblem"),
                ItemType("MysticEmblem"),
                ItemType("NinjaEmblem")
            }));


            //Gems
            RecipeGroup.RegisterGroup("Gems", new RecipeGroup(() => Lang.misc[37] + " Gem", new int[]
            {
                ItemID.Amethyst,
                ItemID.Topaz,
                ItemID.Ruby,
                ItemID.Sapphire,
                ItemID.Emerald,
                ItemID.Ruby,
                ItemID.Diamond,
                ItemID.Amber
            }));

            //Gold Bars
            RecipeGroup.RegisterGroup(GOLD_BARS_GROUP, new RecipeGroup(() => Lang.misc[37] + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            }));

            //Silver Bars
            RecipeGroup.RegisterGroup("SilverBars", new RecipeGroup(() => Lang.misc[37] + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            }));

            //Titanium Bars
            RecipeGroup.RegisterGroup("TitaniumBars", new RecipeGroup(() => Lang.misc[37] + " Titanium Bar", new int[]
            {
                ItemID.TitaniumBar,
                ItemID.AdamantiteBar
            }));

            //Large Gems
            RecipeGroup.RegisterGroup("LargeGems", new RecipeGroup(() => Lang.misc[37] + " Large Gem", new int[]
            {
                ItemID.LargeAmethyst,
                ItemID.LargeTopaz,
                ItemID.LargeSapphire,
                ItemID.LargeEmerald,
                ItemID.LargeRuby,
                ItemID.LargeDiamond,
                ItemID.LargeAmber
            }));

            //Dungeon Tables
            RecipeGroup.RegisterGroup("DungeonTables", new RecipeGroup(() => Lang.misc[37] + " Dungeon Table", new int[]
            {
                ItemID.GothicTable, //Gothic
                ItemID.BlueDungeonTable, //Blue
                ItemID.GreenDungeonTable, //Green
                ItemID.PinkDungeonTable  //Pink
            }));
        }
        #endregion

        public override void Load()
        {
            instance = this;
            zaWarudo = 0;

            if (!Main.dedServ)
            {                                                                                            //Foreground Filter (RGB)
                Filters.Scene["Laugicality:Etherial"] = new Filter(new EtherialShader("FilterMiniTower").UseColor(0.1f, 0.4f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["Laugicality:Etherial"] = new EtherialVisual();
                Filters.Scene["Laugicality:Etherial2"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(0f, 2f, 8f).UseOpacity(0.8f), EffectPriority.VeryHigh);
                Filters.Scene["Laugicality:ZaWarudo"] = new Filter(new ZaShader("FilterMiniTower").UseColor(0.5f, .5f, .5f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["Laugicality:ZaWarudo"] = new ZaWarudoVisual();

                // Register a new music box
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DuneSharkron"), ItemType<DuneSharkronMusicBox>(), TileType<Tiles.MusicBoxes.DuneSharkronMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Hypothema"), ItemType<HypothemaMusicBox>(), TileType<Tiles.MusicBoxes.HypothemaMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Obsidium"), ItemType<ObsidiumMusicBox>(), TileType<Tiles.MusicBoxes.ObsidiumMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Ragnar"), ItemType<RagnarMusicBox>(), TileType<Tiles.MusicBoxes.RagnarMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RockPhase3"), ItemType<DioritusMusicBox>(), TileType<Tiles.MusicBoxes.DioritusMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AnDio"), ItemType<AnDioMusicBox>(), TileType<Tiles.MusicBoxes.AnDioMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Annihilator"), ItemType<AnnihilatorMusicBox>(), TileType<Tiles.MusicBoxes.AnnihilatorMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Slybertron"), ItemType<SlybertronMusicBox>(), TileType<Tiles.MusicBoxes.SlybertronMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SteamTrain"), ItemType<SteamTrainMusicBox>(), TileType<Tiles.MusicBoxes.SteamTrainMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Etheria"), ItemType<EtheriaMusicBox>(), TileType<Tiles.MusicBoxes.EtheriaMusicBox>());
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiumSurface"), ItemType<GreatShadowMusicBox>(), TileType<Tiles.MusicBoxes.GreatShadowMusicBox>());


                MysticaUI = new LaugicalityUI();
                MysticaUI.Activate();
                MysticaUserInterface = new UserInterface();
                MysticaUserInterface.SetState(MysticaUI);
            }

            toggleMystic = RegisterHotKey("Toggle Mysticism", "Mouse2");
            toggleSoulStoneV = RegisterHotKey("Toggle Visual Effects", "V");
            toggleSoulStoneM = RegisterHotKey("Toggle Mobility Effects", "C");
            quickMystica = RegisterHotKey("Quick Mystica", "G");
            soulStoneAbility = RegisterHotKey("Soul Stone Ability (1)", "Mouse3");
        }

        public override void PostSetupContent()
        {
            #region BossChecklist

            Mod bossChecklist = ModLoader.GetMod("BossChecklist");

            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "The Annihilator", 9.991f, (Func<bool>)(() => LaugicalityWorld.downedAnnihilator), string.Format("The Steam-O-Vision [i:{0}] will summon it at night", ItemType<MechanicalMonitor>()));
                bossChecklist.Call("AddBossWithInfo", "Slybertron", 9.992f, (Func<bool>)(() => LaugicalityWorld.downedSlybertron), string.Format("The Steam Crown [i:{0}] calls to its King", ItemType<SteamCrown>()));
                bossChecklist.Call("AddBossWithInfo", "Steam Train", 9.993f, (Func<bool>)(() => LaugicalityWorld.downedSteamTrain), string.Format("A Suspicious Train Whistle [i:{0}] might get its attention.", ItemType<SuspiciousTrainWhistle>()));
                bossChecklist.Call("AddBossWithInfo", "Dune Sharkron", 2.3f, (Func<bool>)(() => LaugicalityWorld.downedDuneSharkron), string.Format("A Tasty Morsel [i:{0}] in the desert will attract this Shark's attention.", ItemType<TastyMorsel>()));
                bossChecklist.Call("AddBossWithInfo", "Hypothema", 3.8f, (Func<bool>)(() => LaugicalityWorld.downedHypothema), string.Format("There's a chill in the air... [i:{0}]", ItemType<ChilledMesh>()));
                bossChecklist.Call("AddBossWithInfo", "Ragnar", 4.5f, (Func<bool>)(() => LaugicalityWorld.downedRagnar), string.Format("This Molten Mess [i:{0}] guards the Obsidium.", ItemType<MoltenMess>()));
                bossChecklist.Call("AddBossWithInfo", "Etheria", 11.51f, (Func<bool>)(() => LaugicalityWorld.downedTrueEtheria), string.Format("The guardian of the Etherial will consume her prey. Can only be called at night.[i:{0}]", ItemType<EmblemOfEtheria>()));
                bossChecklist.Call("AddBossWithInfo", "Dioritus", 5.91f, (Func<bool>)(() => LaugicalityWorld.downedAnDio), string.Format("This  [i:{0}] calls the brother of the Guardians of the Underground", ItemType<AncientAwakener>()));
                bossChecklist.Call("AddBossWithInfo", "Andesia", 5.92f, (Func<bool>)(() => LaugicalityWorld.downedAnDio), string.Format("The brother calls for his sister."));
            }

            #endregion

            #region AchMod

            Mod achMod = ModLoader.GetMod("AchievementLibs");

            int[] rewardsBleedingHeart = { ItemType("ObsidiumHeart") };
            int[] rewardsBleedingHeartCount = { 1 };

            if (achMod != null)
            {
                achMod.Call("AddAchievementWithoutAction", this, "A Bleeding Heart", string.Format("Defeat Ragnar, Guardian of the Obsidium.  [i:{0}]", ItemType<MoltenMess>()), "Achievements/ragChieve2", rewardsBleedingHeart, rewardsBleedingHeartCount, (Func<bool>)(() => LaugicalityWorld.downedRagnar));
                //achMod.Call("AddAchievementWithoutReward", this, "The Bleeding Heart Guardian", string.Format("Defeat Ragnar, Guardian of the Obsidium.  [i:{0}]", ItemType("MoltenMess")), "Achievements/ragChieve2", (Func<bool>)(() => LaugicalityWorld.downedRagnar));
            }

            #endregion

            FocusManager.Instance.PostSetupFocuses();
        }

        public override void Unload()
        {
            instance = null;
        }

        public override void UpdateMusic(ref int music, ref MusicPriority musicPriority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu)
            {
                if (Main.player[Main.myPlayer].active && Main.player[Main.myPlayer].GetModPlayer<LaugicalityPlayer>(this).zoneObsidium)
                {
                    if (Main.player[Main.myPlayer].ZoneOverworldHeight || Main.player[Main.myPlayer].ZoneSkyHeight)
                        music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/ObsidiumSurface");
                    else
                        music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/Obsidium");

                    musicPriority = MusicPriority.BiomeHigh;
                }

                if (LaugicalityWorld.downedEtheria)
                {
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/Etherial");
                    musicPriority = MusicPriority.Environment;
                }
            }

            if (zaWarudo > 0)
                zaWarudo--;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

            if (mouseTextIndex != -1)
                layers.Insert(mouseTextIndex, new EnigmaMysticaInterface(MysticaUI));
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            int zTime = reader.ReadInt32();
            zaWarudo = zTime;
            Main.NewText(zTime.ToString(), 150, 50, 50);

            EnigmaMessageType msgType = (EnigmaMessageType)reader.ReadByte();

            switch (msgType)
            {
                case EnigmaMessageType.ZaWarudoTime:
                    int zTime2 = reader.ReadInt32();
                    zaWarudo = zTime2;
                    Main.NewText(zTime2.ToString(), 150, 50, 50);
                    break;
                default:
                    ErrorLogger.Log("Laugicality: Unknown Message type: " + msgType);
                    break;
            }
        }

        enum EnigmaMessageType : byte
        {
            ZaWarudoTime,
        }

        public UserInterface MysticaUserInterface { get; private set; }
        public LaugicalityUI MysticaUI { get; private set; }
    }

}
