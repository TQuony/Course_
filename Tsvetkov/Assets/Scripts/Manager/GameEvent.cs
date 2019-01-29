public static class GameEvent
{
    public const string HP_UPDATED = "HP_UPDATED";              //изменения Хп игрока
    public const string BATTLE_COMPLETE = "BATTLE_COMPLETE";
    public const string GAME_OVER = "GAME_OVER";
    public const string SET_UI = "SET_UI";                      //установить ползунки баров на сцене battle
    public const string HP_UPDATED_ENEMY = "HP_UPDATED_ENEMY";  //изменения Хп врага
    public const string SEE_DECK = "SEE_DECK";                  //показать колоду
    public const string SEE_INVENTORY = "SEE_INVENTORY";        //показать инвентарь
    public const string CLOSE_DECK = "CLOSE_DECK";              //закрыть колоду
    public const string CLOSE_INVENTORY = "CLOSE_INVENTORY";    //закрыть инвентарь
    public const string SEE_EQUIPPED = "SEE_EQUIPPED";          //показать итем для оборудования
    public const string CLOSE_EQUIPPED = "CLOSE_EQUIPPED";      //закрыть итем для оборудования
    public const string SEE_UNEQUIPPED = "SEE_UNEQUIPPED";      //показать итем, чтобы убрать из слота экипировки
    public const string UNEQUIPPED = "UNEQUIPPED";              //снять карту из экипировки
    public const string ADD_EQUIPPED = "ADD_EQUIPPED";          //добавить итем в пустой слот экипировки
    public const string OPEN_REWARD = "OPEN_REWARD";
    public const string SET_MANA = "SET_MANA";                  //установить ману игрока
    public const string MANA_STR_UPDATED = "MANA_STR_UPDATED";  //изменить манапул по силе
    public const string MANA_INT_UPDATED = "MANA_INT_UPDATED";  //изменить манапул по интелекту
    public const string MANA_AGI_UPDATED = "MANA_AGI_UPDATED";  //изменить манапул по ловкости
}