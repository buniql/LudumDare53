using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float NeededPackageAmount;
    public float MovementSpeed;
    public float ActivationDistance;
    public int AttackDamage;
    public float AttackRange;
    public float AttackCooldown;
    [HideInInspector]
    public bool canAttack = true;
    [HideInInspector]
    public GameObject Player;
    public GameObject PrefabOnDeath;
    public GameObject CoinPrefab;
    public int CoinDropAmount;
    [HideInInspector]
    public Vector3 direction;
    
    private bool activated = false;
    [HideInInspector]
    public Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        transform.localScale *= Random.Range(.8f, 1.2f);
        Player = GameObject.Find("Player");
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        direction = Player.transform.position - transform.position;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (direction.sqrMagnitude > 1f) direction.Normalize();

        if (Vector3.Distance(transform.position, Player.transform.position) < ActivationDistance) activated = true;

        if (activated)
        {
            Move();
        }

        if (NeededPackageAmount == 0)
        {
            Die();
        }
        
        if(Vector3.Distance(transform.position, Player.transform.position) < AttackRange && canAttack)
        {
            StartCoroutine((Attack()));
        }
    }

    public virtual void Die()
    {
        for(int i = 0; i < CoinDropAmount; i++)
        {
            Instantiate(CoinPrefab, transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), -1) + Vector3.down * 3, Quaternion.identity);
        }
        var spawn = Instantiate(PrefabOnDeath, transform.position, Quaternion.identity);
        spawn.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        _rigidbody2D.MovePosition(transform.position + direction * MovementSpeed * Time.fixedDeltaTime);
    }

    public virtual IEnumerator Attack()
    {
        canAttack = false;
        Player.GetComponent<PlayerController>().DamagePlayer(AttackDamage);
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
    }

    public virtual void DecreaseNeededPackageAmount()
    {
        NeededPackageAmount -= 1;
    }
}
