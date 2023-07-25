using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Animator anim;
    //infor player
    [SerializeField] private float maxHealth=100;
    [SerializeField] private float currentHealth;
    public float damagePerSlash;
    [SerializeField] private UltimateJoystick joystick;
    [SerializeField] PlayerState State;
    enum PlayerState
    {
        Idle,
        Run,
        Attack,
        Die
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        float h = joystick.GetHorizontalAxis();
        float v = joystick.GetVerticalAxis();
        rb.velocity = new Vector3(h, 0, v) * speed;
        if (h != 0 || v != 0)
        {
            State = PlayerState.Run;
            anim.SetBool("run",true);
        }
        if (h == 0 && v == 0)
        {
            State = PlayerState.Idle;
            anim.SetBool("run",false);
        }
        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
        }
        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("attack", false);
    }
    public void takeDamage(float damageAmout)
    {
        currentHealth -= damageAmout;
        if (currentHealth <= 0)
        {
            
        }
    }
}
