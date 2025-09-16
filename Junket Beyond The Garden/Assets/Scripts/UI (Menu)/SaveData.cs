using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public Vector3 playerPosition;   // 👈 consistent name + capitalization
    public string mapBoundary;       // 👈 spelling fixed

    public List<InventorySaveData> inventorySaveData;

}

