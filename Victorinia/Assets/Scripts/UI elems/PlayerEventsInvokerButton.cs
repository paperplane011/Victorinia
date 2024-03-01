using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerEventsInvokerButton : MonoBehaviour
{
    [SerializeField] private PlayerEventsInvoker.EventType _eventToInvoke;
    
    [SerializeField] private bool _boolArg;

    public bool _hasArgs;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(RaiseEvent);   
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }


    private void RaiseEvent()
    {
        if (_hasArgs)
        {
            PlayerEventsInvoker.RaiseEvent(_eventToInvoke, _boolArg);
        }
        else
        {
            PlayerEventsInvoker.RaiseEvent(_eventToInvoke);
        }
    }


}
