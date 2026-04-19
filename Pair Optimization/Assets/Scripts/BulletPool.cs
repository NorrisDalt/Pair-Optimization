using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 20;

    private List<GameObject> bullets = new List<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public void SpawnBullet(Vector3 position, Vector3 aimDirection)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.SetActive(true);

                bullet.GetComponent<Bullet>().SetDirection(aimDirection);
                return;
            }
        }
    }
}
