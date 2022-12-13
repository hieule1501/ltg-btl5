using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 6f;

    Transform player;

    BehaviorTree behaviorTree;
    public SharedFloat sharedSpeed;
    bool hasUpgradeSpeed;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    private void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        sharedSpeed.Value = speed;
        behaviorTree.SetVariable("MovingSpeed", sharedSpeed);
        behaviorTree.RegisterEvent("UpgradeSpeed", UpgradeSpeed);
        hasUpgradeSpeed = false;
    }

    void UpgradeSpeed()
    {
        if (!hasUpgradeSpeed)
        {
            sharedSpeed.Value = speed * 2f;
            behaviorTree.SetVariable("MovingSpeed", sharedSpeed);
            hasUpgradeSpeed = true;
        }

    }
}
