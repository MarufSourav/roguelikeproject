using UnityEngine;

public abstract class RifleBaseState
{
    public abstract void EnterState(RifleStateManager USM);
    public abstract void UpdateState(RifleStateManager USM);
}
