using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    public DashAbility()
    {
        abilityType = AbilityType.NO_AIMING;
    }

    public override void SetDirection(Vector2 _direction)
    {
        
    }

    public override void SetPosition(Vector3 _position)
    {
        
    }

    public override void SetType()
    {
        abilityType = AbilityType.NO_AIMING;
    }

    public override void UseAbility()
    {
        if (player.CanDash)
        {
            player.audioSource.clip = AudioClip;
            player.audioSource.Play();

            player.CanDash = false;
            player.speed += 3.0f;
            player.DelayDash();
        }

    }
}
