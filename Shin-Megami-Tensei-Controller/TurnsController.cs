using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei.Configs;

namespace Shin_Megami_Tensei;

public class TurnsController
{
    private int _fullTurnsUsed;
    private int _fullTurnsUsedDuringThisAction;
    private int _blinkinTurnsoObtainedDuringThisAction;

    private int _blinkinTurnsCounter;
    private int _blinkinTurnsUsedDuringThisAction;
    
    private int _originalFullTurns;

    private ImplementedConsoleView _view;
    

    public TurnsController(ImplementedConsoleView view)
    {
        _view = view;
    }
    
    public void ChangeTurnsStateWhenPassOrSummon()
    {
        if ( _blinkinTurnsCounter == 0)
        {
            _fullTurnsUsed +=1;
            _fullTurnsUsedDuringThisAction = 1;
            _blinkinTurnsoObtainedDuringThisAction =1;
            _blinkinTurnsCounter = 1;
        }

        else
        {
            _blinkinTurnsCounter -=1;
            _blinkinTurnsUsedDuringThisAction = 1;
        }
     
    }

    public void ChangeTurnsForWeakAffinity()
    {

        if ( CalculateNumberOfFullTurnsLeft()  >= 1)
        {
            _fullTurnsUsed +=1;
            _fullTurnsUsedDuringThisAction = 1;
            _blinkinTurnsoObtainedDuringThisAction =1;
            _blinkinTurnsUsedDuringThisAction = 0;
            _blinkinTurnsCounter += 1;
        }

        else
        {
            _blinkinTurnsCounter -=1;
            _blinkinTurnsUsedDuringThisAction = 1;
        }
    }
    
    public void AnounceTurnsState()
    {
        _view.AnounceTurnsState(_fullTurnsUsedDuringThisAction, _blinkinTurnsUsedDuringThisAction, _blinkinTurnsoObtainedDuringThisAction);
    }
    
    public int CalculateNumberOfFullTurnsLeft()
    {

        int fullTurnsLeft = _originalFullTurns - _fullTurnsUsed ;
        return fullTurnsLeft;
    }
    
    public int CalculateNumberOfBlinkingTurns()
    {

        return _blinkinTurnsCounter;
    }
    
    public void RestartTurns(List<UnitData> team, TeamController teamController)
    {

        
        _fullTurnsUsed = 0;
        _originalFullTurns = teamController.GetActiveUnitsAlive(team).Count;
        _blinkinTurnsCounter = 0;
    }
    
    public void ShowNumberOfTurns()
    {

        _view.ShowNumberOfTurns(CalculateNumberOfFullTurnsLeft(),_blinkinTurnsCounter);
    }
    
    public void ChangeTurnStateForMissNeutralOrResistAffinity()
    {

        if (_blinkinTurnsCounter > 0)
        {
            _blinkinTurnsCounter -= 1;
            _blinkinTurnsUsedDuringThisAction = 1;
        }

        else
        {
            _fullTurnsUsed +=1;
            _fullTurnsUsedDuringThisAction = 1;
        }
        

    }

    public void ChangeTurnsStateForNullAffinity()
    {

        if (_blinkinTurnsCounter == 0)
        {
            _fullTurnsUsedDuringThisAction = Math.Min(2, CalculateNumberOfFullTurnsLeft());
            _fullTurnsUsed += _fullTurnsUsedDuringThisAction;
        }
        
        else if (_blinkinTurnsCounter == 1)
        {
            _blinkinTurnsUsedDuringThisAction = 1;
            _blinkinTurnsCounter -= 1;
            _fullTurnsUsedDuringThisAction = Math.Min(1, CalculateNumberOfFullTurnsLeft());
            _fullTurnsUsed += _fullTurnsUsedDuringThisAction;

        }
        
        else if (_blinkinTurnsCounter >= 2)
        {
            _blinkinTurnsCounter-= 2;
            _blinkinTurnsUsedDuringThisAction = 2;
        }

    }

    public void ChangeTurnsStateForDrOrRepelAffinity()
    {

        _blinkinTurnsUsedDuringThisAction = _blinkinTurnsCounter;
        _fullTurnsUsedDuringThisAction = CalculateNumberOfFullTurnsLeft();
        _blinkinTurnsCounter = 0;
        _fullTurnsUsed = _originalFullTurns;
    }

    public void ChangeTurnsForNonOffensiveAbilities()
    {
        if (_blinkinTurnsCounter > 0)
        {
            _blinkinTurnsCounter -= 1;
            _blinkinTurnsUsedDuringThisAction = 1;
        }
        else
        {
            _fullTurnsUsed +=1;
            _fullTurnsUsedDuringThisAction = 1;
        }
    }

    public void RestartTurnValuesForUnitTurn()
    {

        _fullTurnsUsedDuringThisAction = 0;
        _blinkinTurnsUsedDuringThisAction = 0;
        _blinkinTurnsoObtainedDuringThisAction = 0;
    }

   
    
    
}