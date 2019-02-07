using UnityEngine;
using UnityEngine.UI;
using InterfaceNamespace;
public class ControllerMenu : MonoBehaviour {

    [SerializeField] private Button btnContinue;
    private ISaveManager _saveManager;

    void Start ()
    {
        if(_saveManager.CheckLoad())
        {
            btnContinue.interactable = true;
        }
        
    }

    public void SetDependecies(ISaveManager saveManager)
    {
        _saveManager = saveManager;
    }
}