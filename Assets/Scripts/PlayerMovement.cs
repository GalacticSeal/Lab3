using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement2D
{
    void FixedUpdate()
    {
        velocityX = Input.GetAxisRaw("Horizontal")*speed;
        velocityY = Input.GetAxisRaw("Vertical")*speed;
        transform.position = new Vector2(transform.position.x+velocityX, transform.position.y+velocityY);

        DetectTerrain();
        DetectBorder();
        if(DetectEnemy()) {
            Respawn();
        }
    }
}
