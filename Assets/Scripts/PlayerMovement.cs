using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement2D
{
    Vector2 move;

    void FixedUpdate()
    {
        velocityX = Input.GetAxisRaw("Horizontal")*speed;
        velocityY = Input.GetAxisRaw("Vertical")*speed;
        move = new Vector2(velocityX,velocityY);
        transform.position = (Vector2) transform.position + Vector2.ClampMagnitude(move,speed);

        DetectTerrain();
        DetectBorder();
        if(DetectEnemy()) {
            Respawn();
        }
    }
}
