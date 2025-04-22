using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("UI")]
    public Image keyIcon;

    [Header("INVENTORY PARAMETERS")]
    private GameObject _player;
    private Inventorry _inventorry;
    [SerializeField] private int _uses;


    [Header("TURN EFFECTS")]
    public float _turnSpeed;
    private float _curAng = 0.0f;

    [Header("HOP EFFECTS")]
    public float _hopSpeed;
    public float _hopTop;
    public float _hopBottom;
    private float _currentHop = 0.0f;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _deltaPosition;
    private bool _up = true;

    [Header("Audio")]
    public GameObject _model;
    private AudioSource _audioSource;
    public AudioClip _pickUpAudioClip;
    public AudioClip _hoverAudioClip;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>().gameObject;
        _inventorry = GameObject.FindObjectOfType<Inventorry>();
    }

    void FixedUpdate()
    {
        if (_startPosition == Vector3.zero)
        {
            return;
        }
        
        //ROTATION ANIMATION
        _curAng += _turnSpeed * Time.deltaTime;
        if (_curAng >= 360)
        {
            _curAng -= 360.0f;
        }
        gameObject.transform.rotation = Quaternion.Euler(-90.0f,0.0f,_curAng);

        //HOP ANIMATION
        _deltaPosition = transform.position - _startPosition;
        
        if (_up)
        {
            if (_deltaPosition.y < _hopTop)
            {
                _currentHop = _hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = false;
            }
        }
        else
        {
            if (_deltaPosition.y > _hopBottom)
            {
                _currentHop = -_hopSpeed * Time.deltaTime;
            }
            else
            {
                _up = true;
            }
        }

        gameObject.transform.position += new Vector3(0.0f,_currentHop, 0.0f); 
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _player)
        {
            _inventorry.TakePickUp(this.gameObject, _uses);
        }

        if (other.gameObject.layer == 31)
        {
            _startPosition = transform.position + new Vector3(0.1f,0.1f,0.1f);
            gameObject.GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationX 
                                                                | RigidbodyConstraints.FreezePositionY 
                                                                | RigidbodyConstraints.FreezeRotationZ;
            //Debug.Log(_startPosition);
        }
        //Debug.Log(other.gameObject.layer);
    }

    IEnumerator TakeKey()
    {
        if (_pickUpAudioClip != null)
        {
            Debug.Log("START CORRUTINE");
            //_audioSource.clip = _pickUpAudioClip;
            //_audioSource.pitch = Random.Range(0.9f, 1.1f);
            //_audioSource.volume = 1.0f;
            //_audioSource.Play();
            keyIcon.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.0f);

            this.gameObject.SetActive(false);
        }

        Debug.Log("End wait");
        this.gameObject.SetActive(false);
    }

    public void PickUpEfect()
    {
        StartCoroutine(TakeKey());
    }
}
