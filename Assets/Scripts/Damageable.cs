using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    public bool Alive = true;

    public virtual void TakeDamage(float damage)
    {
        if (!Alive) return;

        _health -= damage;

        if (_health <= 0f)
        {
            Alive = false;
            gameObject.SetActive(false);
        }
    }
}