namespace Shin_Megami_Tensei_View;

public interface IView
{
    void AnounceThatPlayerWon(string samuraiName, string playerNumber);
    void ShowBothTeams(List<string> firstTeamLines, List<string> secondTeamLines);
    void AnounceGunDamage(UnitData unitDataAttacking, UnitData targetUnitData);
    void ShowSelectAbilityMenu(UnitData unitDataAttacking);
    int GetNumericSelectedOptionFromUser();
    void AnounceHPFinalStateForUnit(UnitData unitData);
    string[] ShowTeamsToSelect(string teamsFolder);
    void AnounceThatTeamIsInvalid();
    UnitData SelectMonsterToSummonMenu(List<UnitData> team);
    UnitData GetMonsterToSummonFromBenchWhenItCanBeDead(List<UnitData> team);
    void AnounceThatTargetUnitIsWeak(UnitData targetUnitData, UnitData unitDataAttacking);
    void AnounceThatTargetIsRessistent(UnitData targetUnitData, UnitData unitDataAttacking);
    void AnounceThatTargetBlockedAttack(UnitData targetUnitData, UnitData unitDataAttacking);
    void AnounceThatTargetUnitAbsorbedDamage(UnitData targetUnitData, int damage);
    void AnounceThatTargetUnitReflectedDamage(UnitData targetUnitData, UnitData unitDataAttacking, int baseDamage);
    void ShowSeparator();
    void AnounceAttack(UnitData unitDataAttacking, UnitData targetUnitData);
    void AnounceDamageReceived(UnitData target, int damageWithAffinities);
    UnitData SelectTarget(List<UnitData> oponentUnits, UnitData unitDataAttacking);
    void AnounceEffectOfDamage(UnitData unitDataAttacking, UnitData target, int damageWithAffinities);
    void AnounceGunTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceAttackTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceFireTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceIceTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceElecTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceForceTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceHealAllyTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceReviveAllyTarget(UnitData unitDataAttacking, UnitData target);
    void AnounceHealResult(int heal, UnitData target);
    UnitData GetAllyTarget(UnitData unitDataAttacking, List<UnitData> activeUnitsAlive);
    UnitData GetDeadAllyTarget(UnitData unitDataAttacking, List<UnitData> deadUnits);
    void AnounceRound(string samuraiName, string playerNumber);
    void AnounceThatAMonsterHasBeenSummon(UnitData monsterToSummon);
    void AnounceAttackDamage(UnitData unitDataAttacking, UnitData targetUnitData);
    void AnounceSurrender(string samuaraiName, string playerNumber);
    UnitData GetMonsterToGetOut(List<UnitData> activeUnits);
    void AnounceUnitsOrderOfAction(List<UnitData> activeUnitsInOrder);
    void ShowActionMenu(UnitData unitData);
    void ShowActionsForSamurai();
    void ShowActionsForMonster();
    void AnounceRevive(UnitData attackingUnitData, UnitData allyUnitData);
    void AnounceTurnsState(int fullTurnsUsedDuringThisAction, int blinkinTurnsUsedDuringThisAction, int blinkinTurnsoObtainedDuringThisAction);
    void ShowNumberOfTurns(int numberOfFullTurnsLeft, int blinkingTurnsCounter);
}
