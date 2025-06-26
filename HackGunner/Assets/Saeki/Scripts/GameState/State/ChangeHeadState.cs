using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeadState : GameState
{
    StateManager stateManager;
    StateObjectPool objectPool;
    public ChangeHeadState(StateManager stateManager, StateObjectPool objectPool)
    {
        this.stateManager = stateManager;
        this.objectPool = objectPool;
    }

    public void Enter()
    {

    }

    public void StateUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Z))       
        //    stateManager.ChangeState(new FeverTimeState(stateManager, objectPool));

        //else if(objectPool.afterPeopleManager.IsClearFlag)
        //    stateManager.ChangeState(new FeverTimeState(stateManager, objectPool));

        //else if (objectPool.gameoverController.IsGameoverFlag)
        //    stateManager.ChangeState(new GameoverState(stateManager, objectPool));
    }

    public void Exit()
    {
        // gameObject.SetActive(false);
    }
}
