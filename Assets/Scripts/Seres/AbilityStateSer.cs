using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStateSer : StateBaseSer
{
    Ability ability;

    public AbilityStateSer(SerController serController, Ability _ability) : base(serController)
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
            if (Mathf.Approximately(serController.abilityDirection.x, 0f) && Mathf.Approximately(serController.abilityDirection.y, 0f))
            {
                serController.aimingArrow.SetActive(false);
            }

            if ((serController.input.ability1XIsUp || serController.input.ability2AIsUp) && !(Mathf.Approximately(serController.abilityDirection.x, 0f) && Mathf.Approximately(serController.abilityDirection.y, 0f)))
            {
                serController.SetState(new MovementSerState(serController));
            }
        } else
        {
            serController.SetState(new MovementSerState(serController));
        }
    }

    public override void OnStateEnter()
    {
        serController.rb.velocity = Vector2.zero;

        if (ability.AbilityType == AbilityType.AIMING)
            serController.aimingArrow.SetActive(true);

        if (serController.abilityDirection == Vector2.zero)
        {
            serController.abilityDirection = Vector2.left;
        }
    }

    public override void OnStateExit()
    {
        ability.SerController = serController;

        if (ability.AbilityType == AbilityType.AIMING) { 
            ability.SetDirection(serController.abilityDirection);
            ability.SetPosition(serController.transform.position); //Esto cambiar para evitar problemas con colliders
            ability.UseAbility();

            //serController.abilityDirection = Vector2.zero;
            serController.aimingArrow.transform.localPosition = serController.abilityDirection;
        } else if(ability.AbilityType == AbilityType.NO_AIMING)
        {
            ability.UseAbility();
        }

        serController.aimingArrow.SetActive(false);
    }

    private void AimController()
    {
        if (!Mathf.Approximately(serController.input.horizontal, 0f) || !Mathf.Approximately(serController.input.vertical, 0f))
        {
            serController.aimingArrow.SetActive(true);
            serController.abilityDirection.Set(serController.input.horizontal, serController.input.vertical);
            serController.abilityDirection.Normalize();
        } else if (serController.abilityDirection != Vector2.zero)
        {
            serController.aimingArrow.SetActive(true);
        }



        serController.aimingArrow.transform.localPosition = serController.abilityDirection * 1.15f;
    }

}
