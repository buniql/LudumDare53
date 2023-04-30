using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEnemyController : EnemyController
{
    public override void Die()
    {
        var spawn1 = Instantiate(PrefabOnDeath, transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0), Quaternion.identity);
        spawn1.transform.rotation = transform.rotation;
        var spawn2 = Instantiate(PrefabOnDeath, transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0), Quaternion.identity);
        spawn2.transform.rotation = transform.rotation;
        Destroy(gameObject);
        
    }
}
