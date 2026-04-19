using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        // simple bounds check
        if (Mathf.Abs(transform.position.x) > 5f ||
            Mathf.Abs(transform.position.y) > 5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnumTagComponent>(out var enumTag) && enumTag.tagValue == EnumTag.Enemy)
        {
            other.GetComponent<Enemy>().TakeHit();
            gameObject.SetActive(false);
        }
    }
}
