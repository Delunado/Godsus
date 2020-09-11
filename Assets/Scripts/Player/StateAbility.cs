using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAbility : StateBase
{
    Ability ability;

    public StateAbility (Player player, Ability _ability) : base(player)
    {
        ability = _ability;
    }

    public override void Tick()
    {
        if (ability.AbilityType == AbilityType.AIMING)
            AimController();

        ChangeState();
    }

    private void ChangeState()
    {
        if (ability.AbilityType == AbilityType.AIMING)
        {
            if (player.input.ability1XIsUp || player.input.ability2AIsUp)
            {
                player.SetState(new StateMovement(player));
            }
        } else
        {
            player.SetState(new StateMovement(player));
        }
    }

    public override void OnStateEnter()
    {
        player.rb.velocity = Vector2.zero;
    }

    public override void OnStateExit()
    {
        if (ability.AbilityType == AbilityType.AIMING)
        {
            ability.SetDirection(player.abilityDirection);
            ability.SetPosition(player.transform.position); //Esto cambiar para evitar problemas con colliders
            ability.UseAbility();

            player.abilityDirection = Vector2.zero;
            player.aimingArrow.transform.localPosition = player.abilityDirection;
        } else
        {
            ability.Player = player;
            ability.UseAbility(); 
        }

    }

    private void AimController()
    {
        if (!Mathf.Approximately(player.input.horizontal, 0f) || !Mathf.Approximately(player.input.vertical, 0f))
        {
            player.abilityDirection.Set(player.input.horizontal, player.input.vertical);
            player.abilityDirection.Normalize();
        }

       player.aimingArrow.transform.localPosition = player.abilityDirection * 1.15f;
    }


}
