using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    public GameObject PackagePrefab;
    public Transform PackageSpawnLocation;
    public float SpawnCooldown = 5f;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Cooldown());
            var spawned = Instantiate(PackagePrefab, PackageSpawnLocation.position + RandomVector2Offset(), Quaternion.identity);
            spawned.GetComponent<PackageMovement>().Deactivate();
        }

    }

    private Vector3 RandomVector2Offset()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1);
    }

    private IEnumerator Cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(SpawnCooldown);
        canSpawn = true;
    }
}
