using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Push")]
public class PushAbility : Ability
{
    [SerializeField] GameObject pushGO;

    public override void UseAbility()
    {
        ProjectileController pushController = Instantiate(pushGO, position, Quaternion.identity).GetComponent<ProjectileController>();
        pushController.Direction = direction;
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

