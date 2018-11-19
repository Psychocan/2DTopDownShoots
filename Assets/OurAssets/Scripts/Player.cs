using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : LivingEntity {

    [Header("Weapon")]
    [Tooltip("The players melee weapon")]
    [SerializeField] private MeleeWeapon sword;
    [Header("Movement")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float slowDown = 10;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    [Header("Sprite Shite")]
    [SerializeField] private SpriteRenderer jetPack;
    [SerializeField] private SpriteRenderer flames;
    [Header("Animator")]
    [SerializeField] private Animator animCont;
    [Header(" Ignore This ")]
    [SerializeField] private GameObject myChild;


    private Camera cam;
    private Rigidbody2D rb2D;
    private Vector2 moveVelocity;
    private Vector2 lastVel;
    private float lastTime;
    private float lerpTime;

    protected override void Start () {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    
    protected override void Update () {
        base.Update();
        if (!Dead) {
            Move();
            Attack();
            DirectionFacing();
        }
	}

    private void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Move() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.sqrMagnitude <= 0) {
            if (moveVelocity.sqrMagnitude > 0) { moveVelocity = Vector2.Lerp(lastVel, Vector2.zero, lerpTime); lerpTime += slowDown * Time.deltaTime; }
        }

        else { moveVelocity = moveInput.normalized * speed; lastVel = moveVelocity; lerpTime = 0; }
        
    }

    //sprite rotation
    private void DirectionFacing() {
        //jetpacks ordering needs 0 for facing down, 1 for facing up
        //flame ordering needs -3 for face down, 0 for face up

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane gp = new Plane(Vector3.forward, Vector3.zero);
        float rayDist;

        if (gp.Raycast(ray, out rayDist)) {
            Vector3 point = ray.GetPoint(rayDist);
            myChild.transform.position = point;

            //check the direction the player is looking and change the animations and sprites to match
            if (myChild.transform.localPosition.y <= 0) {
                if (animCont != null) { animCont.SetTrigger("down"); }
                if (myChild.transform.localPosition.x >= 0) {
                    /*facing down right*/
                    if (jetPack != null) { jetPack.sortingOrder = 0; }
                    if (flames != null) { flames.sortingOrder = -3; }
                    transform.localScale = new Vector3(1, 1, 1);
                } else {
                    /*facing down left*/
                    if (jetPack != null) { jetPack.sortingOrder = 1; }
                    if (flames != null) { flames.sortingOrder = 0; }
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            } else {
                if (animCont != null) { animCont.SetTrigger("up"); }
                if (myChild.transform.localPosition.x >= 0) {
                    /*facing up right*/
                    if (jetPack != null) { jetPack.sortingOrder = 0; }
                    if (flames != null) { flames.sortingOrder = -3; }
                    transform.localScale = new Vector3(1, 1, 1);
                } else {
                    /*facing up left*/
                    if (jetPack != null) { jetPack.sortingOrder = 1; }
                    if (flames != null) { flames.sortingOrder = 0; }
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            myChild.transform.position = point;

            if (sword != null) { sword.transform.LookAt(point); }
        }


    }



    private void Attack() {
        if (!Input.GetMouseButton(0))
            return;
        //if (sword != null) { if(sword.canSwing) { sword.Swing(); }}
    }



}
