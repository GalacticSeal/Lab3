using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour 
{
    protected float velocityX = 0f;
    protected float velocityY = 0f;

    protected float spawnPosX;
    protected float spawnPosY;

    protected bool allowInput = true; //determines whether the entity can control their character at the given time, but does not affect physics simulation
    protected bool stasis = false; //for temporarily putting the entity's movement in a stasis where they can't move

    [SerializeField] protected float speed = 0.1f;

    [SerializeField] protected string terrainMaskLayer = "Terrain";
    [SerializeField] protected string enemyMaskLayer = "Enemy";
    [SerializeField] protected string borderMaskLayer = "Border";

    protected Vector2 entitySize;
    protected Vector2 entityDisplace;
    protected LayerMask terrainMask, enemyMask, borderMask;
    protected RaycastHit2D[] hitArray = new RaycastHit2D[2];

    protected BoxCollider2D hitbox;

    public void Respawn() {
        transform.position = new Vector2(spawnPosX,spawnPosY);
        velocityX = 0f;
        velocityY = 0f;
        allowInput = true;
        stasis = false;
    }

    public void SetSpawn(float xPos, float yPos) {
        spawnPosX = xPos;
        spawnPosY = yPos;
    }

    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        entitySize = hitbox.bounds.size;
        entityDisplace = hitbox.bounds.center - transform.position;
        terrainMask = LayerMask.GetMask(terrainMaskLayer);
        enemyMask = LayerMask.GetMask(enemyMaskLayer);
        borderMask = LayerMask.GetMask(borderMaskLayer);

        spawnPosX = transform.position.x;
        spawnPosY = transform.position.y;
    }

    protected bool DetectEnemy() {
        RaycastHit2D hit = Physics2D.BoxCast(hitbox.bounds.center, entitySize*0.9f, 0f, new Vector2(0f,0f), entitySize.x*0.9f/2, enemyMask, -0.1f, 0.1f);
        return hit.collider != null;
    }

    protected void DetectTerrain() {
        //Detect all current terrain collisions
        hitArray = Physics2D.BoxCastAll(hitbox.bounds.center, entitySize, 0f, new Vector2(0f,0f), entitySize.x/2f, terrainMask, -0.1f, 0.1f);
        
        for(int i = 0; i < hitArray.Length; i++) {

            Vector2 contactPoint = hitArray[i].point;
            switch((int) Mathf.Ceil(hitArray[i].normal.y)) { //Positive y on normal means the collision was below due to the normal being outward from the collision point
                case -1:
                    if(velocityY > 0) { //needs to be moving up for this collision
                        velocityY = 0;
                        transform.position = new Vector2(transform.position.x, contactPoint.y-entitySize.y/2f-entityDisplace.y); //snap to ceiling upon collision
                        break;
                    } else {
                        break;
                    }
                case 1:
                    if(velocityY < 0) { //needs to be moving down for this collision
                        velocityY = 0;
                        transform.position = new Vector2(transform.position.x, contactPoint.y+entitySize.y/2f-entityDisplace.y); //snap to floor upon collision
                        break;
                    } else {
                        break;
                    }
                default:
                    break;
            }

            //Check for horizontal collision data
            switch((int) Mathf.Ceil(hitArray[i].normal.x)) { //Positive x on normal means the collision was from the left . . . this becomes more confusing than verticals
                case -1:
                    if(velocityX > 0) { //needs to be moving right for this collision
                        velocityX = 0;
                        transform.position = new Vector2(contactPoint.x-entitySize.x/2f-entityDisplace.x, transform.position.y); //snap to left wall upon collision
                        break;
                    } else {
                        break;
                    }
                case 1:
                    if(velocityX < 0) { //needs to be moving left for this collision
                        velocityX = 0;
                        transform.position = new Vector2(contactPoint.x+entitySize.x/2f-entityDisplace.x, transform.position.y); //snap to right wall upon collision
                        break;
                    } else {
                        break;
                    }
                default:
                    break;
            }
        }
    }

    protected void DetectBorder() {
        //Detect all current terrain collisions
        hitArray = Physics2D.BoxCastAll(hitbox.bounds.center, entitySize, 0f, new Vector2(0f,0f), entitySize.x/2f, borderMask, -0.1f, 0.1f);
        
        for(int i = 0; i < hitArray.Length; i++) {

            Vector2 contactPoint = hitArray[i].point;
            switch((int) Mathf.Ceil(hitArray[i].normal.y)) { //Positive y on normal means the collision was below due to the normal being outward from the collision point
                case -1:
                    if(velocityY > 0) { //needs to be moving up for this collision
                        velocityY = 0;
                        transform.position = new Vector2(transform.position.x, contactPoint.y-entitySize.y/2f-entityDisplace.y); //snap to ceiling upon collision
                        break;
                    } else {
                        break;
                    }
                case 1:
                    if(velocityY < 0) { //needs to be moving down for this collision
                        velocityY = 0;
                        transform.position = new Vector2(transform.position.x, contactPoint.y+entitySize.y/2f-entityDisplace.y); //snap to floor upon collision
                        break;
                    } else {
                        break;
                    }
                default:
                    break;
            }

            //Check for horizontal collision data
            switch((int) Mathf.Ceil(hitArray[i].normal.x)) { //Positive x on normal means the collision was from the left . . . this becomes more confusing than verticals
                case -1:
                    if(velocityX > 0) { //needs to be moving right for this collision
                        velocityX = 0;
                        transform.position = new Vector2(contactPoint.x-entitySize.x/2f-entityDisplace.x, transform.position.y); //snap to left wall upon collision
                        break;
                    } else {
                        break;
                    }
                case 1:
                    if(velocityX < 0) { //needs to be moving left for this collision
                        velocityX = 0;
                        transform.position = new Vector2(contactPoint.x+entitySize.x/2f-entityDisplace.x, transform.position.y); //snap to right wall upon collision
                        break;
                    } else {
                        break;
                    }
                default:
                    break;
            }
        }
    }
}