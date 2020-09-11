using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBaseSer
{
    protected SerController serController;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public StateBaseSer(SerController serController)
    {
        this.serController = serController;
    }
}
