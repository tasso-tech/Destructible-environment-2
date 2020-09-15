using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 target;
    public float speed;
    public GameObject effect;

    public float areaOfEffect;
    public LayerMask whatIsDestructible;
    public int damage;

     void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

     void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, target) < 0.1f)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enviroment"))
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                objectsToDamage[i].GetComponent<DestructibleEnvi>().health -= damage;
            }
            Instantiate(effect, transform.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
