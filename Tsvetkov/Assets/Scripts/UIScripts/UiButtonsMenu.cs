using InterfaceNamespace;
using UnityEngine;


public class UiButtonsMenu : MonoBehaviour {
    
    private ISaveManager _saveManager;
    private IAnimaManager _animaManager;

	public void ToNewGame () {

        if (_saveManager.CheckLoad())
        {
            var parent = transform.parent.parent;
            _animaManager.SetStateAnima(parent.gameObject, "new_if_save", true);
            _animaManager.SetStateAnima(parent.gameObject, "open", false);
        }
        else
        {
            var parent = transform.parent;
            _animaManager.SetStateAnima(parent.gameObject, "fading", true);
        }
    }
    public void ToContinueGame()
    {
        _animaManager.SetStateAnima(transform.parent.parent.gameObject, "fading", true);
    }
    public void ToSayNo()
    {
        var parent = transform.parent.parent;
        _animaManager.SetStateAnima(parent.gameObject, "open", false);
        _animaManager.SetStateAnima(parent.gameObject, "new_if_save", false);
    }
    public void ToSayYes()
    {
        _saveManager.DeleteSave();
        _animaManager.SetStateAnima(transform.parent.parent.gameObject, "fading", true);
    }
    public void SetDependecies(ISaveManager saveManager, IAnimaManager animaManager)
    {
        _saveManager = saveManager;
        _animaManager = animaManager;
    }

}
