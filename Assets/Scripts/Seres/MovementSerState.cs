using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSerState : StateBaseSer
{
    public MovementSerState(SerController serController) : base(serController) {}

    public override void Tick()
    {
        Vector3 velocity = new Vector3(serController.input.horizontal, serController.input.vertical, 0);
        serController.rb.velocity = velocity.normalized * serController.speed;
        serController.LastDirection = 1 * (int)Mathf.Sign(serController.input.horizontal);
        serController.animator.SetBool("Walking", true);

        if (!Mathf.Approximately(velocity.x, 0))
        {
            if (serController.LastDirection > 0)
            {
                serController.spriteSer.flipX = true;
                serController.abilitySprite1.flipX = true;
                serController.abilitySprite2.flipX = true;

            }
            else if (serController.LastDirection < 0)
            {
                serController.spriteSer.flipX = false;
                serController.abilitySprite1.flipX = false;
                serController.abilitySprite2.flipX = false;
            }
        }

        if (Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.y, 0))
        {
            serController.animator.SetBool("Walking", false);
        }

        ChangeState();

    }

    private void ChangeState()
    {
        if (serController.input.ability1XIsDown)
        {
            serController.SetState(new AbilityStateSer(serController, serController.ability1));
            return;
        }

        if (serController.input.ability2AIsDown)
        {
            serController.SetState(new AbilityStateSer(serController, serController.ability2));
            return;
        }
    }

    public override void OnStateEnter()
    {
        serController.animator.SetBool("Walking", true);
    }

    public override void OnStateExit()
    {
        serController.animator.SetBool("Walking", false);
    }

}
