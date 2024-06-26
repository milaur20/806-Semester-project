using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBehaviour : MonoBehaviour
{
    public List<GameObject> collection;
    public GameObject[] slots;

    private GameObject currentSlot;

    [SerializeField] private bool debugging;

    void Start()
    {
        // Find and assign each slot with a corresponding slot GameObject
        for (int i = 0; i < slots.Length; i++)
        {
            GameObject slot = GameObject.Find("slot" + (i + 1)); // Assuming coins are named as "slot1", "slot2", etc.
            if (slot != null)
            {
                slots[i] = slot;
                if(debugging)
                {
                    Debug.Log("Assigned " + slot.name + " to slot " + (i + 1));
                }
            }
            else
            {
                if(debugging)
                {
                    Debug.LogError("Slot not found for slot " + (i + 1));
                }
            }
        }
    }

    void Update()
    {
        if(collection.Count > 0)
        {
            foreach (GameObject obj in collection)
            {
                if (obj != null)
                {
                    if(debugging)
                    {
                        Debug.Log("Updating " + obj.name);
                    }

                    //get the texture variable of the object, and assign it to the slots texture
                    slots[collection.IndexOf(obj)].GetComponent<RawImage>().texture = obj.GetComponent<MedalBehavior>().texture;

                    //Update the text of the object
                    //obj.GetComponentInChildren<TextMesh>().text = textList[collection.IndexOf(obj)];
                    
                }
            }
        }
    }

    public void AddToCollection(GameObject obj)
    {
        if(debugging)
        {
            Debug.Log("Adding " + obj.name + " to collection");
        }
        
        //check if object is already in collection
        if (collection.Contains(obj))
        {
            if(debugging)
            {
                Debug.LogWarning(obj.name + " is already in collection");
            }
            
            return;
        }
        collection.Add(obj);
        //collection.OrderBy(go => go.name).ToList();
    }
}
