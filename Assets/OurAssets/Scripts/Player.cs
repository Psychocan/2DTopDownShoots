using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : LivingEntity {

    [Tooltip("The players melee weapon")]
    [SerializeField] private MeleeWeapon sword;

    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;

    private Rigidbody2D rb2D;
    private Vector2 moveVelocity;
    private Vector2 lastVel;
    private float lastTime;
    private float lerpTime;

    protected override void Start () {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    protected override void Update () {
        base.Update();
        if (!Dead) {
            Move();
            Attack();
        }
	}

    private void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Move() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.sqrMagnitude <= 0) {
            if (moveVelocity.sqrMagnitude > 0) { moveVelocity = Vector2.Lerp(lastVel, Vector2.zero, lerpTime); lerpTime += Time.deltaTime; }
        }

        else { moveVelocity = moveInput.normalized * speed; lastVel = moveVelocity; lerpTime = 0; }
        
    }

    private void Attack() {
        if (!Input.GetMouseButton(0))
            return;
        //if(sword.canSwing) {
            //sword.Swing();
        //}
    }



}
