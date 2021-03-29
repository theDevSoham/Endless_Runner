using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManagement : MonoBehaviour
{
    public GameObject[] tileManage; //stores number of tiles to spawn randomly

    Transform player;
    private float spawnTile = 0.0f; //spawning tile after the position of player >= the value of this variable

    readonly private float tileLen = 50.07f;    //length of one tile

    readonly private int amtTiles = 6;  //min amount of tiles after which repeat occurs

    private int indexOfTile;    //for random indexing of tiles

    private List<GameObject> usedTiles; //record of previous tiles


    // Start is called before the first frame update
    void Start()
    {
        usedTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnTile(0);   //initial tile with no difficulty

        //instantiating min amount of tiles to be constant
        for(int i = 1; i <= amtTiles; i++)
        {
            indexOfTile = Random.Range(0, tileManage.Length);
            SpawnTile(indexOfTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > (spawnTile - amtTiles * tileLen))
        {
            indexOfTile = Random.Range(0, tileManage.Length);
            SpawnTile(indexOfTile);
            DeleteTile();
        }
    }



    private void SpawnTile(int index)
    {
        GameObject go;
        go = Instantiate(tileManage[index]);
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnTile;
        spawnTile += tileLen;
        usedTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(usedTiles[0]);
        usedTiles.RemoveAt(0);
    }
}
