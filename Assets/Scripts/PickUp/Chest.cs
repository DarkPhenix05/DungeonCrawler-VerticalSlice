using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Chest : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _keyBoard;
    [SerializeField] private GameObject _gamePad;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _key;
    [SerializeField] private List<GameObject> _reward;
    [SerializeField] private GameObject _rewardHolder;
    public ParticleSystem _particleSistem;
    
    public Transform _dropTransform;
    
    private GameObject _player;
    private Player _playerScript;
    private bool opened = false;
    
    public AudioSource _audioSource;
    public AudioClip _openClip;
    public AudioClip _rewardClip;
    public AudioClip _closeClip;

    // Start is called before the first frame update
    void Awake()
    {
        _playerScript = FindObjectOfType<Player>();
        _player = _playerScript.gameObject;
        
        _canvas = this.gameObject.GetComponentInChildren<Canvas>();
        _canvas.gameObject.SetActive(false);
        
        //_particleSistem = this.gameObject.GetComponentInChildren<ParticleSystem>();
        
        _camera = Camera.main;

        for (int i = 0; i < _rewardHolder.transform.childCount; i++)
        {
            GameObject temp = _rewardHolder.transform.GetChild(i).gameObject;
            _reward.Add(temp);
            temp.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canvas.gameObject.activeInHierarchy)
        {
            _canvas.transform.rotation =
                Quaternion.LookRotation(_canvas.transform.position-_camera.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerScript.chest = true;
            _playerScript.inRange = true;
            _playerScript.canCollect = _playerScript.HaveKey(_key, this.gameObject);
            Debug.Log(_playerScript.canCollect);
            
            if (!opened)
            {
                _canvas.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            _playerScript.chest = false;
            _playerScript.inRange = false;
            _playerScript.canCollect = false;
            _canvas.gameObject.SetActive(false);
        }
    }

    public void Loked()
    {
        if (!opened)
        {
            _audioSource.PlayOneShot(_closeClip);
        }
    }
    
    public void DropReward()
    {
        StartCoroutine(DropRewardCoroutine());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DropRewardCoroutine()
    {
        opened = true;
        _canvas.gameObject.SetActive(false);

        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_openClip);
        yield return new WaitForSeconds(_openClip.length);
        
        _particleSistem.Pause();
        _particleSistem.gameObject.SetActive(false);
        
        foreach (GameObject t in _reward)
        {
            t.transform.position = 
                new Vector3(_dropTransform.position.x, _dropTransform.position.y, 
                    _dropTransform.position.z + (float)Random.Range(-1.0f,1.0f));
            t.SetActive(true);
            t.GetComponent<Rigidbody>().AddForce(((gameObject.transform.forward) + (gameObject.transform.right * 0.7f)) * 5f, ForceMode.Impulse);
        }
       
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_rewardClip);
    }
}
