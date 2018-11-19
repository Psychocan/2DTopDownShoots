using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable {

    protected bool Dead = false;
    protected int health;
    [SerializeField] protected int maxHealth = 10;

    public void TakeDamage(int _damage) { health -= _damage; }

    protected virtual void Start () { health = maxHealth; }

    protected virtual void Update () { if (health <= 0) { Dead = true; } }
}
