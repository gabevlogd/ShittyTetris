using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public Vector3 SpawnPoint;
    public Block[] BlockPrefabs;
    [HideInInspector]
    public bool SpawnBlock;

    [HideInInspector]
    public static List<Block> PlacedBlocks = new List<Block>();

    private void Awake() => SpawnNewBlock();

    private void Update()
    {
        if (SpawnBlock) SpawnNewBlock();
        //if (Input.GetKeyDown(KeyCode.S)) SpawnNewBlock();
    }


    private void SpawnNewBlock()
    {
        SpawnBlock = false;
        Block block = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)], SpawnPoint, Quaternion.identity);
        block.BlockSpawner = this;
    }
}
