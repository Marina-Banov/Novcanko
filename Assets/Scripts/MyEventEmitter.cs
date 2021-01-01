using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyEventEmitter : MonoBehaviour
{
    public UnityEvent updateCurrentItemEvent;
    public UnityEvent setShowCoinsEvent;

    public void emitUpdateCurrentItemEvent()
    {
        updateCurrentItemEvent.Invoke();
    }

    public void emitSetShowCoinsEvent()
    {
        setShowCoinsEvent.Invoke();
    }
}
