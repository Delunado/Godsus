using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    private Player player;
    private HealthControllerPlayer health;

    private void Awake()
    {
        player = transform.GetComponentInParent<Player>();
        health = GetComponent<HealthControllerPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ProjectileController>())
        {
            health.AddHealth(-collision.GetComponent<ProjectileController>().Damage);
            Destroy(collision.gameObject);
        }
    }
}
