using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isJump = true;
    bool isDead = false;
    int idMove = 0;
    Animator anim;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Idle();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Idle();
        }
        Dead();
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }

    public void MoveRight()
    {
        idMove = 1;
    }

    public void MoveLeft()
    {
        idMove = 2;
    }

    public void Move()
    {
        if (idMove == 1 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 3f, 0, 0);
                transform.localScale = new Vector3(0.1579265f, 0.1579265f, 0.1579265f);
        }
        if (idMove == 2 && !isDead)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 3f, 0, 0);
            transform.localScale = new Vector3(-0.1579265f, 0.1579265f, 0.1579265f);
        }
    }

    private void Jump()
    {
        if (!isJump)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 250f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Coin"))
        {
            Data.score += 10;
            Destroy(collision.gameObject);
        }
    }

    public void Idle()
    {
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }

    private void Dead()
    {
        if (!isDead)
        {
            if (transform.position.y < -10f)
            {
                isDead = true;
            }
        }
    }
}
