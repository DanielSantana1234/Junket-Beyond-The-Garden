using Unity.Cinemachine;
using UnityEngine;

public class MapTrans : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundary;
    [SerializeField] private Direction direction;
    private CinemachineConfiner2D confiner;

    // Correct enum spelling
    private enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        // Grab the 2D confiner
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache();
            UpdatePlayerPosition(collision.gameObject); // lowercase g ✅
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2;
                break;
            case Direction.Down:
                newPos.y -= 2;
                break;
            case Direction.Left:
                newPos.x -= 2; // I flipped this so Left actually goes left
                break;
            case Direction.Right:
                newPos.x += 2;
                break;
        }

        player.transform.position = newPos;
    }
}



