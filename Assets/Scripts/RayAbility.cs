using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ray")]
public class RayAbility : Ability
{
    [SerializeField] private GameObject ray;

    public override void UseAbility()
    {
        if (serController.canUseRay)
        {
            ProjectileController _ray = Instantiate(ray, position, Quaternion.identity).GetComponent<ProjectileController>();
            _ray.Direction = direction;
            _ray.SerController = serController;

            serController.StartCorroutineRay();

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
