using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    // Coroutine (we can call it every XX seconds an infinite nb of times)
    IEnumerator SpawnMonsters()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            // NB: the max index in the Range will be (monsterReference.Length - 1), so that we do not go out of the boundaries of the array
            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            // Create instance of a monster
            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            // Left side
            if (randomSide == 0)
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<Monster>().speed = Random.Range(4, 10);
            }
            // Right side
            else
            {
                spawnedMonster.transform.position = rightPos.position;
                // Invert the movement with a minus sign
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(4, 10);
                spawnedMonster.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

} // class
