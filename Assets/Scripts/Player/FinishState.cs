using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : StateBase
{
    public FinishState(Player player) : base(player) { }

    public override void Tick()
    {
        //Para el que este vivo al finalizar el combate
    }

    public override void OnStateEnter()
    {
        player.transform.position = player.winningPoint.position;
        Camera.main.GetComponent<Animator>().SetBool("Finish", true);
    }
}
