using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePossesion : StateBase
{
    public StatePossesion(Player player) : base(player) { }

    public override void Tick()
    {
        //Desactivar la posesion
        if (player.input.ability4YIsDown || player.possesedSer.Health.IsDead)
        {
            player.SetState(new StateMovement(player));
        }
    }

    public override void OnStateEnter()
    {
        player.possesedSer = player.selectedSer;
        player.selectedSer = null;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;
        player.hitBoxCollider.enabled = false;
        player.possesedSer.StartPossesion(player);
        player.inPossesion = true;
    }

    public override void OnStateExit()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Collider2D>().enabled = true;
        player.hitBoxCollider.enabled = true;
        player.possesedSer.FinishPossesion();
        player.transform.position = player.possesedSer.transform.position;
        player.possesedSer = null;
        player.DelayPossesion(); //No es 100% necesario

        if (player.combinationButtonsNumber < 7)
        {
            player.combinationButtonsNumber++;
        }

    }
}
