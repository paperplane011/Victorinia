using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TopicSelectButton : MonoBehaviour
{

    [SerializeField] private Topic _topic;

    private TopicView _topicView;
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

    private void Awake()
    {
        _topicView = GameAssets.Instance.GetTopicView();
    }


    private void Clicked()
    {
        _topicView.SetTopicVisuals(_topic);
    }



}
