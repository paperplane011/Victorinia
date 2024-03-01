using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectableButtonTween : IdleButtonTween
{

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }


}
