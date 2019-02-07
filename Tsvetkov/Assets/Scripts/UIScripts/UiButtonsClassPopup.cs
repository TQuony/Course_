
using InterfaceNamespace;
using UnityEngine;

public class UiButtonsClassPopup : MonoBehaviour
{
    private IPublisher _publisher;

    private void Start()
    {
        SetDependecies(GameObject.Find("Bootstrapper").GetComponent<Bootstrapper>().publisher);
    }
    public void ToSayNoClass()
    {
        _publisher.Publish(this,"closePopup");
    }
    public void ToSayYesClass()
    {
        _publisher.Publish(this,"selectClass");
        _publisher.Publish(this,"closePopup");
        
    }
    public void SetDependecies( IPublisher publisher)
    {
        _publisher = publisher;
    }

}
