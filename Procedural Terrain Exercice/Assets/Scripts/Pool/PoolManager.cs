using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager singleton;
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> pools;
    [SerializeField]
    private Dictionary<string, GameObject> prefabs;
    [SerializeField]
    private PoolObject[] poolObjects;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        pools = new Dictionary<string, Queue<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
        foreach(PoolObject pool in poolObjects)
        {
            prefabs.Add(pool.type, pool.prefab);
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for(int i=0; i < pool.size; i++)
            {
                GameObject newObj = Instantiate(pool.prefab);
                newObj.SetActive(false);
                objectQueue.Enqueue(newObj);
            }
            pools.Add(pool.type, objectQueue);
        }
        
    }

    public GameObject getFromPool(string type,Vector3 position)
    {
        if(pools.TryGetValue(type,out Queue<GameObject> queue))
        {
            if (queue.Count > 0)
            {
                GameObject result = queue.Dequeue();
                result.SetActive(true);
                result.transform.rotation = Quaternion.identity;
                result.transform.position = position;
              
               
                return result;
            }
            else
            {
                if(prefabs.TryGetValue(type,out GameObject value))
                {
                    GameObject newObject = Instantiate(value);
                    newObject.transform.rotation = Quaternion.identity;
                    newObject.transform.position = position;
          
                    return newObject;
                }
                else
                {
                    return null;
                }
            }
           
        }
        else
        {
            return null;
        }
    }
    public void addToPool(string type,GameObject gameObject)
    {
        if (pools.TryGetValue(type, out Queue<GameObject> queue))
        {
            gameObject.SetActive(false);
            queue.Enqueue(gameObject);
           
        }
      
    }

   
}
