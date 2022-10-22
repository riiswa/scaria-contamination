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
        private ScariaContaminationSettings settings;
        
        public ScariaContamination(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ScariaContaminationSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Chance to give Scaria after a bite.");
            settings.giveScariaChance = listingStandard.Slider(settings.giveScariaChance, 0f, 1f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Scaria Contamination";
        }
    }
}