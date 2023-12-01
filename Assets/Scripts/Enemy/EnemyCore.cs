using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyCore : Damageable
{
    private NavMeshAgent _agent;

    private Transform _destination;

    private Attack _attack;
    private Ray _ray;
    private bool cast;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private float _activateDistance = 10f;

    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private GameObject _coinPref;

    public bool Active { get; set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _attack = GetComponent<Attack>();
        _destination = FindObjectOfType<PlayerCore>().transform;
    }

    private void Start()
    {
        _attack.IsCanShoot = false;
        _attack.SetIgnoreTag("Enemy");
        Active = false;
    }

    private void Update()
    {
        if (!Active) return;
        if (!Alive) return;

        var castDistance = (_destination.position - transform.position).magnitude;

        if (castDistance > _activateDistance) return;

        _agent.SetDestination(_destination.position);

        _ray = new Ray(transform.position, _destination.position - transform.position);

        cast = Physics.Raycast(_ray, castDistance, _layer);

        if (!cast)
        {
            _attack.SetTarget(_destination.position);

            _attack.IsCanShoot = true;
            _agent.stoppingDistance = _distance;
        }
        else
        {
            _attack.IsCanShoot = false;
            _agent.stoppingDistance = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        if (!cast)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(transform.position, _destination.position);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (!Alive)
        {
            var game = FindObjectOfType<GamePlay>();
            game.AddDefeated();
            Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
            Instantiate(_coinPref, pos, Quaternion.identity);
        }
    }
}