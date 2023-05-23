using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hurtLines;
    [SerializeField] private AudioClip[] _punchingLines;
    [SerializeField] private AudioClip[] _voiceLines;
    [SerializeField] private AudioClip _lossVoiceLine;
    [SerializeField] private ShipScript _shipScript;
    [SerializeField] private Animator _shipAnim;

    [Header("SKIP THE CUTSCENE FOR GODS SAKE")]
    [SerializeField] private bool _SkipCutscene;

    [Header("Misc")]
    private SceneHandler _sceneHandler;
    public AudioSource _voice;
    public AudioSource _bossMusicPlayer;
    public AudioClip _claireDeLune;

    public bool _canBeHit = false;
    bool _canPunch = true;

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
        _sceneHandler = GameObject.Find("GameManager").GetComponent<SceneHandler>();
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
        _voice.PlayOneShot(_punchingLines[_randomPicker]);

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

        if(_randomPicker > 6)
        {
            _randomPicker = 0;
        }

        if (_alienHealth == 0)
        {
            _canPunch = false;
            _anim.SetBool("IsDead", true);
            _gunsAnim.SetBool("DeployGuns", false);
            _bossMusicPlayer.Stop();

            StartCoroutine(Die());
            
        }
    }

    IEnumerator Sequencer()
    {
        if(_SkipCutscene == false)
        {
            yield return new WaitForSeconds(3);
            _voice.PlayOneShot(_claireDeLune);
            yield return new WaitForSeconds(0.5f);
            _voice.PlayOneShot(_voiceLines[0]);
            yield return new WaitForSeconds(122);
            _voice.PlayOneShot(_voiceLines[2]);
            yield return new WaitForSeconds(10);
        }
        
        _gunsAnim.SetBool("DeployGuns",true);
        _bossMusicPlayer.Play();

        for(int i = 0; i < 15; i++)
        {
            if (_canPunch)
            {
                yield return new WaitForSeconds(3.5f);
                _canBeHit = true;
                yield return new WaitForSeconds(0.5f);
                DoPunch();
                yield return new WaitForSeconds(2);

                AnimatorClipInfo[] _shipAnimInfo;
                _shipAnimInfo = _shipAnim.GetCurrentAnimatorClipInfo(0);

                if (_shipAnimInfo[0].clip.name == "SpaceIdle")
                {
                    _shipScript.TakeDamage();
                }

                if (_shipScript._shipHealth <= 0)
                {
                    _gunsAnim.SetBool("DeployGuns", false);
                    _voice.PlayOneShot(_lossVoiceLine);
                    _sceneHandler.ChangeScene("TheEnd", 3);

                }
                yield return new WaitForSeconds(1);
                _canBeHit = false;
            }
        }
        
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(4);
        _voice.PlayOneShot(_voiceLines[1]);
        yield return new WaitForSeconds(30);
        _sceneHandler.ChangeScene("Credits");
    }

    
}
