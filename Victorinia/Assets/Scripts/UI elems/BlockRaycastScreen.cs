using System;
using UnityEditor.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BlockRaycastScreen : MonoBehaviour
{

    public static Action<bool> SetBlockRaycastStatus;

    
    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        SetBlockRaycastStatus += SetBlockRaycast;
    }

    private void OnDisable()
    {
        SetBlockRaycastStatus -= SetBlockRaycast;
    }

    private void Start()
    {
        SetBlockRaycast(false);
    }

    private void SetBlockRaycast(bool doesBlockRaycast)
    {
        if (doesBlockRaycast) _canvasGroup.blocksRaycasts = true;
        else _canvasGroup.blocksRaycasts = false;
    }
}


