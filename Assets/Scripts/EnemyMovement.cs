using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement2D
{
    void FixedUpdate() {
        GameObject Player = GameObject.FindWithTag("Player");
        Vector2 move = Player.transform.position - transform.position;
        move = Vector2.ClampMagnitude(move,speed);

        transform.position = (Vector2) transform.position + move;

        DetectTerrain();
    }
}
