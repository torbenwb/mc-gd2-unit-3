using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent eventRaised = new UnityEvent();

    private void Awake()
    {
        if (gameEvent) gameEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        if (gameEvent) gameEvent.UnregisterListener(this);
    }

    public void EventRaised()
    {
        eventRaised.Invoke();
    }
}
