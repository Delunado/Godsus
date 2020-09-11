using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/FireBall")]
public class FireBallAbility : Ability
{
    [SerializeField] private GameObject fireBall;

    public override void UseAbility()
    {
        if (serController.canUseFireBall)
        {
            ProjectileController _fireball = Instantiate(fireBall, position, Quaternion.identity).GetComponent<ProjectileController>();
            _fireball.Direction = direction;
            _fireball.SerController = SerController;

            serController.StartCorroutineFireBall();

            serController.audioSource.clip = AudioClip;
            serController.audioSource.Play();
        }
    }

    public override void SetDirection(Vector2 _direction)
    {
        direction = _direction;
    }

    public override void SetPosition(Vector3 _position)
    {
        position = _position;
    }

    public override void SetType()
    {
        abilityType = AbilityType.AIMING;
    }
}
