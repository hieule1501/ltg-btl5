using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class SphereItem : ItemBase
{ 
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting shoot = collision.gameObject.GetComponentInChildren<PlayerShooting>();
            if (shoot.IsSuper) return;
            shoot.UpgradeAttack();
            ItemManager.Instance.DespawnItem(transform);
        }
    }
}
