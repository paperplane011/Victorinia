using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TopicButton : MonoBehaviour
{

    [SerializeField] private Topic _topic;

    private Button _thisButton;

    [Header("Component hooks")]
    [SerializeField] private TMPro.TextMeshProUGUI _textComp;



    private void OnEnable()
    {
        _thisButton.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
    }



    private void Clicked()
    {

    }


}
