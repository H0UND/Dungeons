using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    [SerializeField]
    private float _radius = 5f;
    private bool _isCast;
    private bool _isSearching;
    [SerializeField]
    private SphereCollider _collider;
    
    private EnemyCore _target;

    public bool IsFound;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        _isSearching = true;
        IsFound = false;
    }

    private void Update()
    {
        if (_isSearching)
        {
            if (_collider.radius < _radius)
            {
                _collider.radius += Time.deltaTime * 10f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isSearching)
        { 
            return;
        }

        var enemy = other.transform.gameObject.GetComponent<EnemyCore>();

        if (enemy != null)
        {

            _isSearching = false;
            _collider.radius = 0f;

            _target = enemy;
            IsFound = true;
        }
        else
        {
            _isSearching = true;
            IsFound = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_isCast)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, _collider.radius);
    }

    public void SetRadius(float radius)
    {
        _radius = radius;
    }
    public void SetSearchRadius(float radius)
    {
        _collider.radius = radius;
    }

    public void StartSearch()
    {
        _isSearching = true;
    }

    public void StopSearch()
    {
        _isSearching = false;
    }

    public EnemyCore GetTarget()
    {
        return _target;
    }
}