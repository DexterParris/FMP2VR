using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _gunAnim;
    [SerializeField] private AlienBoss _alienBoss;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoABarrelRoll(bool _isRight)
    {
        StartCoroutine(BarrelRoll(_isRight));
    }

    IEnumerator BarrelRoll(bool _isRight)
    {
        if (_gunAnim.GetBool("DeployGuns"))
        {
            if (_isRight)
            {
                _anim.SetTrigger("TurnRight");

            }
            else
            {
                _anim.SetTrigger("TurnLeft");
            }

            

            if (_alienBoss._canBeHit)
            {
                yield return new WaitForSeconds(2);
                _gunAnim.SetTrigger("ShootGuns");
                _alienBoss.TakeDamage();
            }
            
        }
        else
        {
            yield return null;
        }


        yield return null;
    }


}
