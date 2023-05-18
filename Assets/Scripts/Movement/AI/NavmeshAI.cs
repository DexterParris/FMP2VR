using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshAI : MonoBehaviour
{
    Animator anim;

    Pathfinding pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        pathfinder = gameObject.GetComponentInParent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementScript.Invincible == true)
        {
            anim.SetBool("Killable", true);

        }
        else
        {
            anim.SetBool("Killable", false);
        }

    }

    public void Die()
    {
        anim.SetBool("Dead", true);
        pathfinder.destPoint = 1;
        pathfinder.nav.speed = 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.tag == "HomePoint")
        {
            anim.SetBool("Killable", false);
            anim.SetBool("Dead", false);
            pathfinder.nav.speed = 5;
        }
    }
}
