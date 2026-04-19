using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 0.25f;
    public Transform firePoint;

    private Vector2 moveInput;
    private Vector2 aimDirection;
    private float fireTimer;

    void Update()
    {
        Move();
        FaceMouse();

        fireTimer += Time.deltaTime;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnShoot()
    {
        // only fire if cooldown is ready
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        // clamp position to map bounds (-5 to 5)
        float clampedX = Mathf.Clamp(transform.position.x, -4.6f, 4.6f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.6f, 4.5f);

        transform.position = new Vector3(clampedX, clampedY, 0);
    }

    void FaceMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        aimDirection = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // offset so "up" of sprite faces cursor
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void Shoot()
    {
        BulletPool.Instance.SpawnBullet(firePoint.position, aimDirection);
    }
}