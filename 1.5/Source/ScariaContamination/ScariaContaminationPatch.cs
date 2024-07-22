using System.Reflection;
using Verse;
using HarmonyLib;
using RimWorld;

namespace ScariaContamination;

[StaticConstructorOnStartup]
public class ScariaContaminationPatch
{
    static ScariaContaminationPatch()
    {
        Harmony harmony = new("riiswa.scariacontamination");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}

[HarmonyPatch(typeof(DamageWorker_AddInjury), "ApplyToPawn")]
public static class DamageWorker_BitePatch
{
    [HarmonyPostfix]
    public static void ApplyPatch(DamageWorker_AddInjury __instance, DamageInfo dinfo, Pawn pawn)
    {
        if (__instance is not DamageWorker_Bite || pawn == null || dinfo.Instigator is not Pawn instigatorPawn) return;
        Hediff scariaOnInstigator = instigatorPawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Scaria);
        Hediff scariaOnTarget = pawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Scaria);
        if (scariaOnInstigator == null ||
            scariaOnTarget != null ||
            !(Rand.Value <= ScariaContamination.settings.giveScariaChance) ||
            !(pawn.RaceProps?.corpseDef?.comps?.Exists(p => p is CompProperties_Rottable) ?? false)) return;
        Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.Scaria, pawn);
        pawn.health?.AddHediff(hediff);
        pawn.mindState?.mentalStateHandler?.TryStartMentalState(pawn.RaceProps.Humanlike
            ? MentalStateDefOf.Berserk
            : MentalStateDefOf.Manhunter);
    }
}
