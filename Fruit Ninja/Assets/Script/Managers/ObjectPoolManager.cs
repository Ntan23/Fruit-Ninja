using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singleton
    public static ObjectPoolManager Instance {get; private set;}

    void Awake() 
    {
        if(Instance==null)
        {
            Instance=this;
        }
	}
    #endregion
    
    #region PoolClass
    [System.Serializable]
    public class Pool
    {
        public GameObject objectToPool;
        public int amountToPool;
        public bool canExpand;
    }
    #endregion
    
    #region ListVariables
    public List<Pool> fruitsPool;
    public List<Pool> bombsPool;
    private List<GameObject> fruitPooledObjects = new List<GameObject>();
    private List<GameObject> bombPooledObjects = new List<GameObject>();
    #endregion

    #region GameOjectVariables
    public GameObject fruitsParent;
    public GameObject bombsParent;
    #endregion

	// Use this for initialization
    void Start() 
    {
        foreach(Pool fruit in fruitsPool) 
        {
            for(int i = 0; i < fruit.amountToPool; i++) 
            {
                GameObject obj = Instantiate(fruit.objectToPool);
                
                obj.transform.SetParent(fruitsParent.transform);
                
                obj.SetActive(false);
                fruitPooledObjects.Add(obj);
            }
        }

        foreach(Pool bomb in bombsPool) 
        {
            for(int i = 0; i < bomb.amountToPool; i++) 
            {
                GameObject obj = Instantiate(bomb.objectToPool);
                
                obj.transform.SetParent(bombsParent.transform);
                
                obj.SetActive(false);
                bombPooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetFruitFromPool()
    {
        int randomIndex = 0;

        for(int i = 0; i < fruitPooledObjects.Count; i++)
        {
            randomIndex = Random.Range(0, fruitPooledObjects.Count - 1);
            if(!fruitPooledObjects[randomIndex].activeInHierarchy) return fruitPooledObjects[randomIndex];
        }

        for(int j = 0; j < fruitPooledObjects.Count; j++)
        {   
            randomIndex = Random.Range(0, fruitsPool.Count - 1);

            if(fruitsPool[randomIndex].canExpand)
            {
                GameObject obj = Instantiate(fruitsPool[randomIndex].objectToPool);

                obj.transform.SetParent(fruitsParent.transform);
                obj.SetActive(false);
                fruitPooledObjects.Add(obj);

                return obj;
            }
        }
        return null;
    }

    public GameObject GetBombFromPool()
    {
        for(int i = 0; i < bombPooledObjects.Count; i++)
        {
            if(!bombPooledObjects[i].activeInHierarchy) return bombPooledObjects[i];
        }

        foreach(Pool pool in bombsPool)
        {   
            if(pool.canExpand)
            {
                GameObject obj = Instantiate(pool.objectToPool);

                obj.transform.SetParent(bombsParent.transform);
                obj.SetActive(false);
                fruitPooledObjects.Add(obj);

                return obj;
            }
        }
        return null;
    }
}
