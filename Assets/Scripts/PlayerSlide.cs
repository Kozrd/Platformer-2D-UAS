using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public bool slide = false;
    
    public PlayerController PL;

    public Rigidbody2D rigidBody;

    public Animator anim;

    public CapsuleCollider2D regularColl;

    public CapsuleCollider2D slideColl;

    public float slideSpeed = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            preformSlide();
    }
    private void preformSlide()
    {
        slide = true;
        anim.SetBool("slide", true);
        regularColl.enabled = false;
        slideColl.enabled = true;
        
        if (!PL.sprite.flipX)
        {
            rigidBody.AddForce(Vector2.right * slideSpeed);
        }
        else
        {
            rigidBody.AddForce(Vector2.left * slideSpeed);
        }
        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetTrigger("idle");
        anim.SetBool("slide", false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        slide = false;
    }
}
