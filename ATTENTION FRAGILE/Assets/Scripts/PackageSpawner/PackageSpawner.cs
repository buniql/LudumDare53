using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    public GameObject PackagePrefab;
    public float SpawnCooldown = 5f;
    private bool canSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(Cooldown());
            var spawned = Instantiate(PackagePrefab, transform.position + RandomVector2Offset(), Quaternion.identity);
            spawned.GetComponent<PackageMovement>().Deactivate();
        }

    }

    private Vector3 RandomVector2Offset()
    {
        return new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
    }

    private IEnumerator Cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(SpawnCooldown);
        canSpawn = true;
    }
}
