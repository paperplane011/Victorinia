using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TopicButton : MonoBehaviour
{
    [SerializeField] private bool _isStartButton; // start button immideatly shows on TopicView
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
        _thisButton = GetComponent<Button>();
    }

    private void Start()
    {
        _textComp.text = _topic.Caption;

        if (_isStartButton)
        {
            _topicView.SetTopic(_topic);
        }

    }


    private void Clicked()
    {
        _topicView.SetTopic(_topic);
        PlayerEventBus.OnTopicViewSet?.Invoke();
    }



}
