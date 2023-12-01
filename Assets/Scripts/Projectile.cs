using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;

    private Vector3 _direction;
    private Collider _collider;
    private string _ignoreTag;

    private float _damage;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void Setting(Vector3 direction)
    {
        _direction = new Vector3(direction.x, direction.y, direction.z);
        transform.LookAt(_direction);

        Destroy(gameObject, 5f);
    }

    public void IgnoreTag(string ignoreTag)
    {
        _ignoreTag = ignoreTag;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ignorable")) return;
        if (other.CompareTag(_ignoreTag)) return;
        if (other.gameObject.GetComponent<Projectile>() != null) return;

        if (other.gameObject.TryGetComponent<Damageable>(out var damageable))
        {
            damageable.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}