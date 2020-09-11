using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitControllerSer : MonoBehaviour
{
    HealthControllerSer health;

    private void Awake()
    {
        health = GetComponent<HealthControllerSer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileController>() && (collision.GetComponent<ProjectileController>().SerController != GetComponentInParent<SerController>() || collision.GetComponent<ProjectileController>().Reflected))
        {
            health.AddHealth(-collision.GetComponent<ProjectileController>().Damage);
            Destroy(collision.gameObject);
        }
    }
}
