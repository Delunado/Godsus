using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMovement : StateBase
{
    public StateMovement(Player player) : base(player) { }

    public override void Tick()
    {
        Vector3 velocity = new Vector3(player.input.horizontal, player.input.vertical, 0);
        player.rb.velocity = velocity.normalized * player.speed;

        ChangeState();
    }

    private void ChangeState()
    {
        if (player.input.ability4YIsDown && player.selectedSer != null && player.canPosses && !player.selectedSer.Possesed)
        {
            player.SetState(new StateCombinationPossesion(player));
            return;
        }

        //if (player.input.ability1XIsDown)
        //{
        //    player.SetState(new StateAbility(player, player.pushAbility));
        //    return;
        //}

        if (player.input.ability2AIsDown)
        {
            player.SetState(new StateAbility(player, player.dashAbility));
            return;
        }
    }

    public override void OnStateEnter()
    {

    }




}
