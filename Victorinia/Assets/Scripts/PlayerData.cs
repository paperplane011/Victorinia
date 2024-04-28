public static class PlayerData 
{

    private static int _money;

    public static int Money { get { return _money; } }


    public static void Initialize()
    {
        _money = 100;
        PlayerEventBus.OnMoneyChanged(_money);
    }


    public static void ChangeMoney(int value)
    {
        _money += value;

        PlayerEventBus.OnMoneyChanged(_money);
    }
}
