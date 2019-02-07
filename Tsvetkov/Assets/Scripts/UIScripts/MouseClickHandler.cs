using UnityEngine;
using UnityEngine.EventSystems;
using InterfaceNamespace;
public class MouseClickHandler : MonoBehaviour, IPointerDownHandler
{
    private bool flagClick;
    private IAnimaManager _animaManager;

    void Start()
    {
        flagClick = false;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!flagClick)
        {
            _animaManager.SetStateAnima(transform.parent.gameObject, "open", true);
            flagClick = true;
        }
    }

    public void SetDependecies(IAnimaManager animaManager)
    {
        _animaManager = animaManager;
    }
}
