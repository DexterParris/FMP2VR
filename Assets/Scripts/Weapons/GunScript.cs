using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : XRGrabInteractable
{
    public GameObject shootingPoint;
    public GameObject particle;
    bool timerIsStarted = false;
    float timeleft;

    //Bayonet related variables
    public Animator _bladeAnim;
    public GameObject _blade;
    
    public InputActionProperty _bladeExtensionButton;

    bool _bladeState = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool _bladeExtension = _bladeExtensionButton.action.WasPressedThisFrame();
        if (_bladeExtension && isSelected)
        {
            if (_bladeState)
            {
                _blade.SetActive(false);
                _bladeAnim.SetBool("Extended", false);
                _bladeState = false;
            }
            else if (_bladeState == false)
            {
                _blade.SetActive(true);
                _bladeAnim.SetBool("Extended", true);
                _bladeState = true;
            }

            
        }

        //detect how long the trigger is being held



        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChargeGun();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            ShootGun();
        }
        */
        

        if (timerIsStarted)
        {
            timeleft += Time.deltaTime;
            
            if(timeleft > 3f)
            {
                timeleft = 3f;

            }
        }

    }

    public void ChargeGun()
    {
        //called in unity when player starts holding the trigger
        print("charging");
        //start the timer here 
        timerIsStarted = true;

        particle.SetActive(true);
    }

    public void ShootGun()
    {
        //called in unity when the player lets go of the trigger

        print("shooting");


        timeleft = Mathf.Round(timeleft * 100f) / 100f;

        //a simple if statement that stops the damage multiplier from being activated if the player only presses the trigger instead of holding it
        if(timeleft <1.2f && timeleft > 1f)
        {
            timeleft = 1f;
        }

        Debug.Log(timeleft);

        timeleft = 1;
        timerIsStarted =false;
        particle.SetActive(false);


        Physics.Raycast(shootingPoint.transform.position, shootingPoint.transform.forward, 10);
        Debug.DrawRay(shootingPoint.transform.position, shootingPoint.transform.forward, Color.red,1f);

    }

    /*void ExtendBlade(InputAction.CallbackContext context)
    {

    }*/

    private IEnumerator DebounceInput(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
