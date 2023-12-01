using UnityEngine;

public class Attack : MonoBehaviour
{
    private Vector3 _target;

    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private float _reloadTime = 3f;

    [SerializeField]
    private float _damage = 3f;

    private float _reloadTimer;

    private float _turmSmoothVelocity;
    private float _turmSmoothTime = 0.1f;
    private string _ignoreTag;

    public bool IsCanShoot { get; set; }
    public bool IsHasTarget { get; set; }

    private void Start()
    {
        _reloadTimer = _reloadTime;
    }

    public void SpawnProjectile()
    {
        var projectile = Instantiate(_projectile, _spawnPosition.position, Quaternion.identity).GetComponent<Projectile>();
        var direction = (_target - _spawnPosition.position).normalized;
        projectile.Setting(direction);
        projectile.IgnoreTag(_ignoreTag);
        projectile.SetDamage(_damage);
        IsHasTarget = false;
    }

    private void Update()
    {
        if (IsCanShoot && IsHasTarget)
        {
            Rotate();
            _reloadTimer += Time.deltaTime;
            if (_reloadTimer >= _reloadTime)
            {
                SpawnProjectile();
                _reloadTimer = 0f;
            }
        }
    }

    private void Rotate()
    {
        var targetpos = _target - transform.position;
        var targetAngle = Mathf.Atan2(targetpos.x, targetpos.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turmSmoothVelocity, _turmSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void SetTarget(Vector3 target)
    {
        IsHasTarget = true;
        _target = target;
    }


    public void SetIgnoreTag(string tag)
    {
        _ignoreTag = tag;
    }
}