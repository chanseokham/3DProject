using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 10;
    private float lifeTime = 0.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > 5.0f)
        {
            Destroy(gameObject, 0.2f);
        }
    }

    public void Shoot(Vector3 direction, float speed)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemyManager = collision.gameObject.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
