using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Start is called before the first frame update
public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T prefab;

    public static GenericObjectPool<T> Instance { get; private set; }
    private Queue<T> objects = new Queue<T>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Method to get pooled object from queue
    /// </summary>
    /// <returns> Object being dequeued </returns>
    public T GetFromPool()
    {
        if (objects.Count == 0)
        {
            AddObjects();
        }

        return objects.Dequeue();
    }

    /// <summary>
    /// Method to add 1 of the desired object to the pool
    /// </summary>
    private void AddObjects()
    {
        var objectToPool = GameObject.Instantiate(
            prefab, this.gameObject.transform);
        objectToPool.gameObject.SetActive(false);
        objects.Enqueue(objectToPool);
    }

    /// <summary>
    /// Overload method to add any number of the desired object to the pool
    /// </summary>
    /// <param name="count"></param>
    public void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var objectToPool = GameObject.Instantiate(
                prefab, this.gameObject.transform);

            objectToPool.gameObject.SetActive(false);

            objects.Enqueue(objectToPool);
        }
    }

    /// <summary>
    /// Method to return object to the pool
    /// </summary>
    /// <param name="objectToReturn"> Object to be returned </param>
    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }
}
