using UnityEngine;

public class BeforePlayState : GameState
{
    StateObjectPool objectPool;
    StateManager stateManager;
    public BeforePlayState(StateManager stateManager, StateObjectPool objectPool)
    {
        this.stateManager = stateManager;
        this.objectPool = objectPool;
    }

    public void Enter()
    {
        return;
    }

    public void StateUpdate()
    {
        if (Input.anyKeyDown)
        {
            stateManager.ChangeState(new MainGameState(stateManager, objectPool));
        }
    }

    public void Exit()
    {
        //objectPool.BeforePlayUIObj.SetActive(false);
    }
}
