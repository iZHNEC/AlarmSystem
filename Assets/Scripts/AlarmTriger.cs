using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            IsOpen?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsOpen?.Invoke(false);
        }
    }

    public UnityAction<bool> IsOpen;
}
