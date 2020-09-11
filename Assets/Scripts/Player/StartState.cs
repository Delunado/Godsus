using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : StateBase
{
    public StartState(Player player) : base(player) { }

    float timer = 0f;
    float delay = 4f;

    public override void Tick()
    {
        if (timer >= delay)
        {
            player.SetState(new StateMovement(player));
            return;
        } else
        {
            timer += Time.deltaTime;
        }
    }

    public override void OnStateEnter()
    {
        //Animacion ganar o efecto
    }
}
