using UnityEngine;
using Verse;

namespace ScariaContamination
{
    public class ScariaContaminationSettings: ModSettings
    {
        public float giveScariaChance = 0.25f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref giveScariaChance, "giveScariaChance", 1f);
            base.ExposeData();
        }
    }

    public class ScariaContamination : Mod
    {
        public static ScariaContaminationSettings settings;

        public ScariaContamination(ModContentPack content) : base(content)
        {
            settings = GetSettings<ScariaContaminationSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new();
            listingStandard.Begin(inRect);
            listingStandard.Label("ScariaContamination_Settings_SpreadChance".Translate(settings.giveScariaChance.ToStringPercent()));
            settings.giveScariaChance = listingStandard.Slider(settings.giveScariaChance, 0f, 1f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ScariaContamination_SettingsCategory".Translate();
        }
    }
}
