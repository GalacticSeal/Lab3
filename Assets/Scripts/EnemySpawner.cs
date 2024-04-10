using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyObj;
    [SerializeField] protected List<GameObject> enemyList = new List<GameObject>();

    [SerializeField] private float rX = 10f; //range on x-axis
    [SerializeField] private float rY = 7f; //range on y-axis

    private float timer;
    [SerializeField] private float interval = 20;

    void Start() {
        timer = 0;
    }

    void FixedUpdate() {
        if(timer < 0) {
            SpawnEnemy();
            timer += interval;
        }
        timer--;
    }


    public void SpawnEnemy() {
        Vector3 spawnPos;
        switch(Random.Range(0,4)) {
            case 0: //top
                spawnPos = new Vector3(Random.Range(-rX, rX), rY, 0);
                break;
            case 1: //right
                spawnPos = new Vector3(rX, Random.Range(-rY, rY), 0);
                break;
            case 2: //bottom
                spawnPos = new Vector3(Random.Range(-rX, rX), -rY, 0);
                break;
            case 3: //left
                spawnPos = new Vector3(-rX, Random.Range(-rY, rY), 0);
                break;
            default: 
                spawnPos = Vector3.zero;
                break;
        }

        GameObject newEnemy = Instantiate(EnemyObj, spawnPos+transform.position, Quaternion.identity);
        enemyList.Add(newEnemy);
    }

    public void DestroyEnemy(GameObject target) {
        Destroy(target);
        enemyList.Remove(target);
    }

    public void DestroyAll() {
        for (int i = enemyList.Count-1; i >= 0; i--) {
            Destroy(enemyList[i]);
            enemyList.RemoveAt(i);
        }
    }
}
