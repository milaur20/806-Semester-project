using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionBehaviour : MonoBehaviour
{
    public List<GameObject> collection;
    public GameObject[] slots;

    private GameObject currentSlot;

    void Start()
    {
        // Find and assign each slot with a corresponding coin GameObject
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject coin = GameObject.Find("coin" + (i + 1)); // Assuming coins are named as "coin1", "coin2", etc.
            if (coin != null)
            {
                slots[i] = coin;
                Debug.Log("Assigned " + coin.name + " to slot " + (i + 1));
            }
            else
            {
                Debug.LogError("Coin not found for slot " + (i + 1));
            }
        }
    }

    // Other methods...

    public void AddToCollection(GameObject obj)
    {
        collection.Add(obj);
        collection.OrderBy(go => go.name).ToList();
    }
}
