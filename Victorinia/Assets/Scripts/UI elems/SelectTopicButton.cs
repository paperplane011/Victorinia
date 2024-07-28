using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectTopicButton : MonoBehaviour
{
    [SerializeField] private bool _isStartButton; // start button immideatly shows on TopicView
    [SerializeField] private Topic _topic;

    private TopicView _topicView;
    private Button _thisButton;

    [Header("Component hooks")]
    [SerializeField] private TMPro.TextMeshProUGUI _captionTextComp;
    [SerializeField] private TMPro.TextMeshProUGUI _completedTextComp;



    private void OnEnable()
    {
        _thisButton.onClick.AddListener(Clicked);

        PlayerEventBus.OnToSelectMenuPressed += UpdateCompletedText;
    }

    private void OnDisable()
    {
        _thisButton.onClick.RemoveAllListeners();
        PlayerEventBus.OnToSelectMenuPressed -= UpdateCompletedText;
    }

    private void Awake()
    {
        _topicView = GameAssets.Instance.GetTopicView();
        _thisButton = GetComponent<Button>();
    }

    private void Start()
    {
        _captionTextComp.text = _topic.Caption;

        if (_isStartButton)
        {
            _topicView.SetTopic(_topic);
        }

    }

    


    private void Clicked()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click);

        _topicView.SetTopic(_topic);
        PlayerEventBus.OnUpdateTopicViewVisuals?.Invoke();
    }


    private void UpdateCompletedText()
    {

        _completedTextComp.text = PlayerData.GetNumOfCompletedDifficultiesOfTopic(_topic.ID) + "/3";


    }



}
