using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 0.35f;
    private CircleCollider2D hitbox;
    private RaycastHit2D[] hit = new RaycastHit2D[10];
    private bool selfDestruct = false;
    [SerializeField] private float maxDistance = 50f;

    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        transform.position += (transform.TransformDirection(Vector3.right)) * speed;
        if (Mathf.Abs(transform.position.x) > maxDistance || Mathf.Abs(transform.position.y) > maxDistance) {
            Destroy(gameObject);
        }
        
        hit = Physics2D.CircleCastAll(hitbox.bounds.center, hitbox.radius, transform.up, hitbox.radius); //checks all collisions
        for(int i = 0; i < hit.Length; i++) {
            if(hit[i].collider != null) {
                if(hit[i].collider.GetComponent<EnemyHealth>() != null) {
                    hit[i].collider.GetComponent<EnemyHealth>().ReceiveDamage();
                    selfDestruct = true;
                }
            }
        }
        if(selfDestruct) {
            Destroy(gameObject);
        }
    }
}
