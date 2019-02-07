using UnityEngine;
using Object = System.Object;

namespace InterfaceNamespace
{
    public interface IPublisher
    {
        void Publish(Object source, Object data);
        void Publish(Object source, Object data, CardsGameClass obj);
        void AddSubscriber(ISubscriber subscriber);
    }
}