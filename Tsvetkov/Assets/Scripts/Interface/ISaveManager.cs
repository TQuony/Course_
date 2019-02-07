

namespace InterfaceNamespace {

    public interface ISaveManager : ISubscriber
    {
         void SetClass(CardsGameClass cardGame);
         bool CheckLoad();
         void DeleteSave();
         void SaveGame();
    }
}