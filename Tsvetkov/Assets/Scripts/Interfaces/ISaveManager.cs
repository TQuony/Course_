using UnityEngine.UI;

namespace DefaultNamespace {
    public interface ISaveManager : IGameManager {
        
        Button btnContinue { get; set; }
        
        void CheckLoad();

        void CheckLevel(int[,] arr);

        void CheclCurLevel(int curLvl);
    }
}