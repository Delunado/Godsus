using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Rebote")]
public class ReboteAbility : Ability
{
    [SerializeField] private GameObject reboteBall;

    public override void UseAbility()
    {
        if (serController.canUseReboteBall)
        {
            ProjectileController _reboteBall = Instantiate(reboteBall, position, Quaternion.identity).GetComponent<ProjectileController>();
            _reboteBall.Direction = direction;
            _reboteBall.SerController = SerController;
            _reboteBall.CanRebotar = true;

            serController.StartCorroutineReboteBall();

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
