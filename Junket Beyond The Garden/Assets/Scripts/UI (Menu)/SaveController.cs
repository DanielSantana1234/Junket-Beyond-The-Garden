using System.IO;
using Unity.Cinemachine;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerposition;  // only one definition!
    public string mapboundary;
}

public class SaveController : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        // Define save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        LoadGame();
    }

    public void SaveGame()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        PlayerSaveData saveData = new PlayerSaveData
        {
            playerPosition = player.transform.position,
            mapBoundary = confiner.BoundingShape2D.gameObject.name // ✅ new API
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(saveLocation));

            // Find player
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = saveData.playerPosition;
            }
            else
            {
                Debug.LogWarning("⚠️ Player not found in scene when loading!");
            }

            // Restore confiner boundary
            var confiner = FindAnyObjectByType<CinemachineConfiner2D>();
            if (confiner != null)
            {
                var boundaryObj = GameObject.Find(saveData.mapBoundary);
                if (boundaryObj != null)
                {
                    var collider = boundaryObj.GetComponent<PolygonCollider2D>();
                    if (collider != null)
                    {
                        confiner.BoundingShape2D = collider;
                    }
                    else
                    {
                        Debug.LogWarning($"⚠️ {saveData.mapBoundary} has no PolygonCollider2D.");
                    }
                }
                else
                {
                    Debug.LogWarning($"⚠️ Map boundary '{saveData.mapBoundary}' not found in scene.");
                }
            }
            else
            {
                Debug.LogWarning("⚠️ No CinemachineConfiner2D found in scene.");
            }

            // Re-attach camera follow/lookAt
            var vcam = FindAnyObjectByType<CinemachineCamera>();
            if (vcam != null && player != null)
            {
                vcam.Follow = player.transform;
                vcam.LookAt = player.transform;
            }
            else
            {
                Debug.LogWarning("⚠️ No CinemachineCamera found in scene or player missing.");
            }
        }
        else
        {
            SaveGame();
        }
    }
}

