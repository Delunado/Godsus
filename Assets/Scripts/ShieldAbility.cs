using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Shield")]
public class ShieldAbility : Ability
{
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
        if (SerController.InShield)
        {
            SerController.InShield = false;
            serController.Shield.SetActive(true);
            SerController.StartCorroutineShield();

            serController.audioSource.clip = AudioClip;
            serController.audioSource.Play();
        }
    }
}
