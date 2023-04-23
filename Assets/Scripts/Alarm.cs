using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AlarmTriger _alarmTriger;
    [SerializeField] private float _recoveryRate = 3f;

    private Coroutine _changeVolue = null;

    private void OnIsOpen(bool flag)
    {
        if (_changeVolue != null)
        {
            StopCoroutine(_changeVolue);
        }

        _changeVolue = StartCoroutine(ChangeVolume(flag));
    }

    private void OnEnable()
    {
        _alarmTriger.IsOpen += OnIsOpen;
    }

    private void OnDisable()
    {
        _alarmTriger.IsOpen -= OnIsOpen;
    }

    private IEnumerator ChangeVolume(bool flag)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        int _requiredValue = flag? (Convert.ToInt32(true)) : (Convert.ToInt32(false));

        if(_audioSource.isPlaying == false && flag == true)
        {
            _audioSource.Play();
        }

        while (_audioSource.volume != _requiredValue)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _requiredValue, Time.deltaTime / _recoveryRate);
            yield return waitForFixedUpdate;
        }

        if(flag == false)
        {
            _audioSource.Stop();
        }
    }
}
