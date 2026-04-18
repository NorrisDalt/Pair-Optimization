using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;

    private Transform player;

    public void SetTarget(Transform target)
    {
        player = target;
        health = 3; // reset when reused from pool
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        float x = Mathf.Clamp(transform.position.x, -5f, 5f);
        float y = Mathf.Clamp(transform.position.y, -5f, 5f);
        transform.position = new Vector3(x, y, 0);
    }

    public void TakeHit()
    {
        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            WaveSpawner.Instance.OnEnemyKilled();
        }
    }
}
