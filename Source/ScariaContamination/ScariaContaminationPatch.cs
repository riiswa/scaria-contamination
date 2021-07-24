using System.Reflection;
using Verse;
using HarmonyLib;
using RimWorld;

namespace ScariaContamination
{
    [StaticConstructorOnStartup]
    class ScariaContaminationPatch
    {
        static ScariaContaminationPatch()
        {
            var harmony = new Harmony("riiswa.scariacontamination");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


    [HarmonyPatch(typeof(DamageWorker_AddInjury), "ApplyToPawn")]
    static class DamageWorker_BitePatch
    {
        [HarmonyPostfix]
        static void ApplyPatch(DamageWorker_AddInjury __instance, DamageInfo dinfo, Pawn pawn)
        {
            if (__instance is DamageWorker_Bite && pawn != null && dinfo.Instigator != null && dinfo.Instigator is Pawn instigatorPawn)
            {
                var scariaOnInstigator = instigatorPawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Scaria);
                var scariaOnTarget = pawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Scaria);
                if (scariaOnInstigator != null && scariaOnTarget == null && Rand.Value <= LoadedModManager
                    .GetMod<ScariaContamination>().GetSettings<ScariaContaminationSettings>().giveScariaChance)
                {
                    var hediff = HediffMaker.MakeHediff(HediffDefOf.Scaria, pawn);
                    pawn.health?.AddHediff(hediff);
                    pawn.mindState?.mentalStateHandler?.TryStartMentalState(pawn.RaceProps.Humanlike
                        ? MentalStateDefOf.Berserk
                        : MentalStateDefOf.Manhunter);
                }
            }
        }
    }
}