public static class PlayerData 
{

    private static int _money;

    public static int Money { get { return _money; } }


    public static void Initialize()
    {
        _money = 100;
        PlayerEventBus.OnMoneyChanged(_money);
    }


    public static bool TryToChangeMoney(int value)
    {
        if(_money + value > 0)
        {
            _money += value;
            PlayerEventBus.OnMoneyChanged(_money);
            return true;
        }
        else
        {
            return false;
        }


        
    }
}
