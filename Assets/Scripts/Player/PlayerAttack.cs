using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerWalk _walk;

    private Attack _attack;

    [SerializeField]
    private PlayerAim _aim;

    [SerializeField]
    private LayerMask _layer;

    private bool _isCast;

    private void Awake()
    {
        _walk = GetComponent<PlayerWalk>();
        _attack = GetComponent<Attack>();
    }

    private void Start()
    {
        _attack.SetIgnoreTag("Player");
    }

    private void Update()
    {
        if (_walk.IsMoving)
        {
            _aim.StopSearch();
            _aim.SetSearchRadius(0);
            _aim.IsFound = false;
            _attack.IsCanShoot = false;
        }
        else
        {
            _aim.StartSearch();
            if (_aim.IsFound)
            {
                _aim.StopSearch();

                var target = _aim.GetTarget();
                _attack.SetTarget(target.transform.position);
                if (!target.Alive)
                {
                    _aim.StartSearch();
                    return;
                }

                var castDistance = (target.transform.position - transform.position).magnitude;
                var _ray = new Ray(transform.position, target.transform.position - transform.position);

                var cast = Physics.Raycast(_ray, castDistance, _layer);
                if (cast)
                {
                    _attack.IsCanShoot = true;
                }
            }
            else
            {
                _attack.IsCanShoot = false;
            }
        }
    }
}