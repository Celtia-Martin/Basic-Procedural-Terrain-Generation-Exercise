using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Blocks", menuName = "ScriptableObjects/Blocks", order = 1)]
public class BlockType : ScriptableObject
{
    public string prefabBlock;
    public BlockTypeEnum blockType;
   
}
public enum BlockTypeEnum
{
    WATER,
    BEACH,
    FIELD,
    FOREST,
    MOUNTAIN

}