using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gold : MonoBehaviour
{
    [Header("INVENTORY PARAMETERS")]
    private GameObject _player;
    private Inventorry _inventorry;
    [SerializeField] private int _value;


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

        _audioSource = this.gameObject.GetComponent<AudioSource>();
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
        gameObject.transform.rotation = Quaternion.Euler(0.0f, _curAng, 0.0f);

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

        gameObject.transform.position += new Vector3(0.0f, _currentHop, 0.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == _player)
        {
            _inventorry.TakeGold(this.gameObject, _value);
        }

        if (other.gameObject.layer == 31)
        {
            _startPosition = transform.position + new Vector3(0.1f, 0.1f, 0.1f);
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX
                                                                | RigidbodyConstraints.FreezePositionY
                                                                | RigidbodyConstraints.FreezeRotationZ;
            //Debug.Log(_startPosition);
        }
        //Debug.Log(other.gameObject.layer);
    }

    IEnumerator TakeGold()
    {
        if(_pickUpAudioClip)
        {
            _audioSource.clip = _pickUpAudioClip;
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.volume = 1.0f;
            _audioSource.Play();
            yield return new WaitForSeconds(_pickUpAudioClip.length);
        }

        this.gameObject.SetActive(false);
    }

    public void PickUpEfect()
    {
        StartCoroutine(TakeGold());
    }
}
