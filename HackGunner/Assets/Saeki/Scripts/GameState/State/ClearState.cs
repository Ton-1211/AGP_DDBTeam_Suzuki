using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearState : GameState
{
    StateManager stateManager;
    StateObjectPool objectPool;
    public ClearState(StateManager stateManager, StateObjectPool objectPool) 
    {
        this.stateManager = stateManager;
        this.objectPool = objectPool;
    }
    public void Enter() 
    {
        //objectPool.timeManager.StartResultStay();
    }
    public void StateUpdate() 
    {
        //if (objectPool.timeManager.GetClearResult)
            stateManager.ChangeState(new ResultState(stateManager, objectPool));
    }
    public void Exit() 
    { 
    
    }
}
