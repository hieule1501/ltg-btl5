using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPosList;
    [SerializeField] private Transform[] _spawnItemList;

    public bool[] _spawnCheckList = new bool[5] { false, false, false, false, false };
    private int itemCount;
    WaitForSeconds spawnInterval = new WaitForSeconds(5f);

    public static ItemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        itemCount = 0;
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return spawnInterval;
            SpawnItem();
        }
    }    

    private void SpawnItem()
    {
        if (itemCount >= 5) return;
        int indexItem = Random.Range(0, _spawnItemList.Length);
        Vector2 randomCircle = Random.insideUnitCircle * 3f;
        for (int i = 0; i < _spawnCheckList.Length; ++i )
        {
            if (_spawnCheckList[i] == false)
            {
                _spawnCheckList[i] = true;
                Vector3 spawnPos = _spawnPosList[i].position + new Vector3(randomCircle.x, 0, randomCircle.y);
                EZ_PoolManager.Spawn(_spawnItemList[indexItem], spawnPos, Quaternion.Euler(new Vector3(-90, 0, 0))).GetComponent<ItemBase>().Index = i;
                itemCount++;
                return;
            }
        }
    }    

    public void DespawnItem(Transform transform)
    {
        EZ_PoolManager.Despawn(transform);
        itemCount--;
        _spawnCheckList[transform.GetComponent<ItemBase>().Index] = false;
    }
}
