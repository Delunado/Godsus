using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/UnderGround")]
public class UnderGroundAbility : Ability
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
        if (SerController.InGround)
        {
            serController.audioSource.clip = AudioClip;
            serController.audioSource.Play();

            SerController.InGround = false;
            SerController.Hitbox.enabled = false;
            SerController.StartCorroutineUnderGround();
            SerController.GetComponent<SpriteRenderer>().color = new Color32(0,0,0,130); ;
        }
    }
}
