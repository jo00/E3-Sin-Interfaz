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

    private View _view;
    

    public TurnsController(View view)
    {
        _view = view;
    }
    
    public void ChangeTurnsStateWhenPassOrSummon()
    {
        Console.WriteLine("turnsController1");
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
        Console.WriteLine("turnsController2");

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
        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Se han consumido {_fullTurnsUsedDuringThisAction} Full Turn(s) y {_blinkinTurnsUsedDuringThisAction} Blinking Turn(s)");
        _view.WriteLine($"Se han obtenido {_blinkinTurnsoObtainedDuringThisAction} Blinking Turn(s)");
    }
    
    public int CalculateNumberOfFullTurnsLeft()
    {
        Console.WriteLine("turnsController3");

        int fullTurnsLeft = _originalFullTurns - _fullTurnsUsed ;
        return fullTurnsLeft;
    }
    
    public int CalculateNumberOfBlinkingTurns()
    {
        Console.WriteLine("turnsController4");

        return _blinkinTurnsCounter;
    }
    
    public void RestartTurns(List<Unit> team, TeamController teamController)
    {
        Console.WriteLine("turnsController5");

        
        _fullTurnsUsed = 0;
        _originalFullTurns = teamController.GetActiveUnitsAlive(team).Count;
        _blinkinTurnsCounter = 0;
    }
    
    public void ShowNumberOfTurns()
    {

        _view.WriteLine("----------------------------------------");
        _view.WriteLine($"Full Turns: {CalculateNumberOfFullTurnsLeft()}");
        _view.WriteLine($"Blinking Turns: {_blinkinTurnsCounter}");
        _view.WriteLine("----------------------------------------");
    }
    
    public void ChangeTurnStateForNeutralOrResistAffinity()
    {
        Console.WriteLine("turnsController7");

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
        Console.WriteLine("turnsController8");

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
        Console.WriteLine("turnsController9");

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
        Console.WriteLine("turnsController10");

        _fullTurnsUsedDuringThisAction = 0;
        _blinkinTurnsUsedDuringThisAction = 0;
        _blinkinTurnsoObtainedDuringThisAction = 0;
    }

   
    
    
}