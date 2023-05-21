using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hurtLines;
    [SerializeField] private AudioClip[] _punchingLines;
    [SerializeField] private AudioClip[] _voiceLines;

    public AudioSource _voice;
    public AudioSource _bossMusicPlayer;
    public AudioClip _claireDeLune;

    public bool _canBeHit = false;

    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _gunsAnim;

    public int _alienHealth = 6;
    int _randomPicker = 0;

    //this is being used for testing
    public bool _testHit;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sequencer());
    }

    // Update is called once per frame
    void Update()
    {
        if (_testHit)
        {
            _testHit = false;
            TakeDamage();
        }




    }

    public void DoPunch()
    {
        _voice.PlayOneShot(_hurtLines[_randomPicker]);

        int punchdirection = Random.Range(0,2);

        switch (punchdirection)
        {
            case 0: 
                _anim.SetTrigger("PunchRight");
                break;

            case 1: 
                _anim.SetTrigger("PunchLeft");
                break;

            default:
                _anim.SetTrigger("PunchLeft");
                break;

        }

       
    }

    public void TakeDamage()
    {
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(1);
        _alienHealth--;
        _voice.PlayOneShot(_hurtLines[_randomPicker]);
        _randomPicker++;

        if (_alienHealth == 0)
        {
            _anim.SetBool("IsDead", true);
            _gunsAnim.SetBool("DeployGuns", false);
            _bossMusicPlayer.Stop();
            _voice.PlayOneShot(_voiceLines[1]);
        }
    }

    IEnumerator Sequencer()
    {
        yield return new WaitForSeconds(3);
        _voice.PlayOneShot(_claireDeLune);
        yield return new WaitForSeconds(0.5f);
        _voice.PlayOneShot(_voiceLines[0]);
        yield return new WaitForSeconds(132);
        _gunsAnim.SetBool("DeployGuns",true);
        _bossMusicPlayer.Play();
    }
}
