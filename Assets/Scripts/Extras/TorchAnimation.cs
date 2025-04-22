using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimation : MonoBehaviour
{
    public Light _light;
    public float _minBrightnes;
    public float _maxBrightnes;

    public float _minWait;
    public float _maxWait;
    private float _waitCount = 0.0f;
    private float _wait;

    private float _curBrightnes;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _wait = Random.Range(_minWait, _maxBrightnes);
        _curBrightnes = (int)Random.Range(_minBrightnes, _maxBrightnes);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _waitCount += Time.deltaTime;

        if (_waitCount >= _wait)
        {
            _curBrightnes = (int)Random.Range(_minBrightnes, _maxBrightnes);
            _waitCount = 0.0f;
        }
        else if(_curBrightnes - Time.deltaTime > _minBrightnes)
        {
            _curBrightnes -= Time.deltaTime;
        }
        else
        {
            _curBrightnes = _minBrightnes;
        }

        _light.intensity = Mathf.PingPong(Time.time, _curBrightnes);
    }
}
