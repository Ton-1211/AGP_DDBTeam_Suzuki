using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultState : GameState
{
    StateManager stateManager;
    StateObjectPool objectPool;

    public ResultState(StateManager stateManager, StateObjectPool objectPool)
    {
        this.stateManager = stateManager;
        this.objectPool = objectPool;
    }

    public void Enter()
    {
        //objectPool.setting.Setting();
        //objectPool.ClearResultUI.SetActive(true);
    }

    public void StateUpdate()
    {
        return;
    }

    public void Exit()
    {
        return;
    }
}
