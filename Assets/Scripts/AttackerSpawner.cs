using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker attackerPrefab;

    bool spawn = true;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAttacker());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StopSpawning()
    {
        spawn = false;
    }

    private IEnumerator SpawnAttacker()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            var newAttacker = Instantiate(attackerPrefab, transform.position, Quaternion.identity);
            newAttacker.transform.parent = this.transform;
            yield return newAttacker;
        }
    }
}
