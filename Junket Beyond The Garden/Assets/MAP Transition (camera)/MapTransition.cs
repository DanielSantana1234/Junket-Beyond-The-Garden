using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [Header("Transition Setup")]
    [SerializeField] private PolygonCollider2D mapBoundary;   // new camera boundary
    [SerializeField] private Transform spawnPoint;            // where to teleport the player

    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Update the camera confiner
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache();

            // Teleport the player to the new spawn
            if (spawnPoint != null)
            {
                collision.transform.position = spawnPoint.position;
            }
            else
            {
                Debug.LogWarning($"No spawn point assigned on {gameObject.name}");
            }
        }
    }

    // Optional: draw a gizmo for the spawn point
    private void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spawnPoint.position, 0.2f);
            Gizmos.DrawLine(transform.position, spawnPoint.position);
        }
    }
}
