using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo
{

    private BlockType blockType;
    private int height;
    private float sizeBlock;
    private int mul = 10;
    private Queue<GameObject> prefabsSpawned;
    public static float waterValue = 0.2f;
    public static float beachValue = 0.1f;
    public static float fieldValue = 0.4f;
    public static float forestValue = 0.2f;
    public static float mountainValue = 0.1f;

    public BlockInfo(float height, BlockType blockType)
    {
        this.height =  blockType.blockType.Equals(BlockTypeEnum.WATER)?1:Mathf.Max((int)((10 * height)-(waterValue*10)+2),1);
        this.blockType = blockType;
        
        sizeBlock = blockType.prefabBlock.GetComponent<MeshRenderer>().bounds.size.y;
        
        prefabsSpawned = new Queue<GameObject>();
    }
    
    public void SpawnBlocks(float x, float y, float z)
    {
        prefabsSpawned.Enqueue(Object.Instantiate(blockType.prefabBlock, new Vector3(x, y + sizeBlock * height, z), Quaternion.identity));
        prefabsSpawned.Enqueue(Object.Instantiate(blockType.prefabBlock, new Vector3(x, y + sizeBlock * height-sizeBlock, z), Quaternion.identity));
        //Filled Terrain:
        //for(int i=1; i <= height; i++) 
        //{
        //    prefabsSpawned.Enqueue(Object.Instantiate(blockType.prefabBlock, new Vector3(x, y + sizeBlock * i,z), Quaternion.identity));
        //}
    }
    public void RemoveBlocks()
    {
        while (prefabsSpawned.Count > 0)
        {
 
            Object.Destroy(prefabsSpawned.Dequeue());
  
        }
    }
    public static BlockTypeEnum getBlockType(float value)
    {
        if (value <= waterValue)
        {
            return BlockTypeEnum.WATER;
        }
        else if (value <= waterValue+beachValue)
        {
            return BlockTypeEnum.BEACH;
        }
        else if (value <= beachValue+fieldValue+waterValue)
        {
            return BlockTypeEnum.FIELD;
        }
        else if( value <= beachValue + fieldValue + forestValue + waterValue)
        {
            return BlockTypeEnum.FOREST;
        }
        else
        {
            return BlockTypeEnum.MOUNTAIN;
        }
    }
}
