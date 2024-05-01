using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public class RewardButton : MonoBehaviour
{

    [SerializeField] private bool _isDoubleReward;
    [SerializeField] private TMPro.TextMeshProUGUI _textComp;

    [SerializeField] private CanvasGroup _continueButtonCanvasGroup;

    private Button _button;
    private int _baseReward;


    private static bool _canGetReward;

    [SerializeField] private CanvasGroup _rewardCanvasGroup;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
        PlayerEventBus.OnRewardSetted += SetRewardBehaviour;
        PlayerEventBus.OnRewardGotten += RewardGottenBehaviour;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        PlayerEventBus.OnRewardSetted -= SetRewardBehaviour;
        PlayerEventBus.OnRewardGotten -= RewardGottenBehaviour;
    }

    private void Start()
    {
        
        CanvasUtils.DisableCanvasGroup(_continueButtonCanvasGroup);
        CanvasUtils.EnableCanvasGroup(_rewardCanvasGroup);
    }

    private void SetRewardBehaviour(int reward)
    {
        if(reward == 0)
        {
            CanvasUtils.DisableCanvasGroup(_rewardCanvasGroup);
            CanvasUtils.EnableCanvasGroup(_continueButtonCanvasGroup);
            return;
        }

        CanvasUtils.EnableCanvasGroup(_rewardCanvasGroup);

        _baseReward = reward;

        CanvasUtils.DisableCanvasGroup(_continueButtonCanvasGroup);

        if (_isDoubleReward) _textComp.text = (_baseReward * 2).ToString();

    }

    private void Clicked()
    {
       


        if (_isDoubleReward)
        {
            PlayerData.TryToChangeMoney(_baseReward * 2);
            YandexGame.RewVideoShow(0);

        }
        else
        {
            PlayerData.TryToChangeMoney(_baseReward);
        }


        PlayerEventBus.OnRewardGotten?.Invoke();

    }


    private void RewardGottenBehaviour()
    {
        

        CanvasUtils.EnableCanvasGroup(_continueButtonCanvasGroup);
        CanvasUtils.DisableCanvasGroup(_rewardCanvasGroup);
    }









}
