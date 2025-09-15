using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        for (int i = 0; i < slotCount; i++) // ✅ fixed loop
        {
            // Create slot
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();

            // Add item if available
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);

                // ✅ fix typo
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                slot.currentItem = item;
            }
        }
    }
}

