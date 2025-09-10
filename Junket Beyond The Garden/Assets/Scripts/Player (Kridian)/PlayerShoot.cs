using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] 
    private GameObject _bulletPrefab;

    [SerializeField] 
    private float _bulletSpeed = 10f;

    [SerializeField] 
    private float _timeBetweenShots = 0.2f; // cooldown

    [SerializeField] 
    private float _initialDelay = 0.5f; // delay before first shot

    private bool _fireContinuously;
    private float _lastFireTime;
    private float _firePressedTime;

    void Update()
    {
        if (_fireContinuously)
        {
            // Has enough time passed since the button was pressed?
            if (Time.time >= _firePressedTime + _initialDelay &&
                Time.time >= _lastFireTime + _timeBetweenShots)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        // Convert mouse position to world space
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0; // important for 2D

        // Calculate direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Spawn bullet
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * _bulletSpeed;
        }
        else
        {
            Debug.LogWarning("No Rigidbody2D found on bullet prefab!");
        }
    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        if (_fireContinuously)
        {
            // Record when the fire button was first pressed
            _firePressedTime = Time.time;
        }
    }
}