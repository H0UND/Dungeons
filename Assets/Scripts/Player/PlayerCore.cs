using UnityEngine;

public class PlayerCore : Damageable
{
    private PlayerWalk _playerWalk;

    public PlayerInput.PlayerActions Input
    {
        get
        {
            _input ??= new PlayerInput();
            return _input.Player;
        }
    }

    private PlayerInput _input;

    private void Awake()
    {
        _playerWalk = GetComponent<PlayerWalk>();
        _playerWalk.SetActions(Input);
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _input ??= new PlayerInput();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (!Alive)
        {
            var game = FindObjectOfType<GamePlay>();
            game.EndGameLos();
        }
    }
}