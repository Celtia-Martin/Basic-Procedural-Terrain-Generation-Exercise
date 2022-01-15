using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PerlinTexturesGenerator :MonoBehaviour
{
    public static PerlinTexturesGenerator singleton;
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    [Range(0.03f,90f)]private float scale;
    private int offset;
    [SerializeField]
    private float sizeBlock=1;
    [SerializeField]
    private BlockType[] blocks;
    [SerializeField]
    private Queue<BlockInfo> spawnedBlocks;
    [SerializeField]
    private GameObject waitingUI;
    private bool generating = false;
    private void Awake()
    {
        if(singleton== null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        //sizeBlock = blocks[0].prefabBlock.GetComponent<MeshRenderer>().bounds.size.y;
        spawnedBlocks = new Queue<BlockInfo>();
        offset = Random.Range(0, 999999);
     
        GenerateWorld();
    }

    public void GenerateWorld() 
    {
        //offset = Random.Range(0, 999999);
        waitingUI.SetActive(true);
        generating = true;

        StartCoroutine(GeneratingWorld());
        StartCoroutine(OnGeneratingDone());
    }
    IEnumerator OnGeneratingDone()
    {
        yield return new WaitUntil(() => !generating);
        waitingUI.SetActive(false);
    }
    IEnumerator GeneratingWorld()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                BlockInfo newBlock = GetNewBlock(GetPerlinNoiseValue(i, j));
                newBlock.SpawnBlocks(i * sizeBlock, 0, j * sizeBlock);
                spawnedBlocks.Enqueue(newBlock);

            }
        }
        generating = false;

    }

   
    public float GetPerlinNoiseValue(int x, int y)
    {
        float newX = (float)x / width *scale +offset;
        float newY = (float)y / height*scale +offset;
        return Mathf.PerlinNoise(newX, newY);

    }
    public BlockInfo GetNewBlock(float perlinValue)
    {
        BlockTypeEnum type = BlockInfo.getBlockType(perlinValue);
        return new BlockInfo(perlinValue, blocks[(int)type]);
    }
    public void RemoveWorld()
    {
        while (spawnedBlocks.Count > 0)
        {
            spawnedBlocks.Dequeue().RemoveBlocks();
        }
    }
}

