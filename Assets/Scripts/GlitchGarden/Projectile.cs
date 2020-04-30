using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float damage = 100f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var attacker = other.GetComponent<Attacker>();
        var health = other.GetComponent<Health>();

        if (attacker && health)
        {
            health.DealDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
