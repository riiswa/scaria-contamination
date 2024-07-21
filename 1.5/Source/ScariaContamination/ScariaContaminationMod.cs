using Verse;
using UnityEngine;
using HarmonyLib;

namespace ScariaContamination;

public class ScariaContaminationMod : Mod
{
    public static Settings settings;

    public ScariaContaminationMod(ModContentPack content) : base(content)
    {

        // initialize settings
        settings = GetSettings<Settings>();
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new Harmony("Taggerung.rimworld.ScariaContamination.main");	
        harmony.PatchAll();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return "ScariaContamination_SettingsCategory".Translate();
    }
}
