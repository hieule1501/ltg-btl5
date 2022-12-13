using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class HeartItem : ItemBase
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health.IsSuper) return;
            health.HealHP();
            ItemManager.Instance.DespawnItem(transform);
        }
    }
}
