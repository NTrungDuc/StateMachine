using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damageSlash;
    public bool isPlayer = false;
    float time = 0;
    float timeAttack = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        getDamageSword();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
    public void getDamageSword()
    {

        if (gameObject.tag == "bladePlayer")
        {
            damageSlash = 10;
            isPlayer = true;
        }
        if (gameObject.tag == "bladeEnemy")
        {
            damageSlash = 2;
            isPlayer = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.gameObject.tag == "Enemy")
            {
                BotController botController = other.gameObject.GetComponent<BotController>();
                if (time < timeAttack)
                {
                    botController.takeDamage(damageSlash);
                }
                else
                {
                    time = 0;
                }

            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                playerMovement player = other.GetComponent<playerMovement>();
                if (time < timeAttack)
                {
                    player.takeDamage(damageSlash);
                }
                else
                {
                    time = 0;
                }
            }
        }
    }
}
