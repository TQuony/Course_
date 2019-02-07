using System;

namespace InterfaceNamespace
{
    public interface ISubscriber
    {
        void OnEvent(Object messageData);
        void OnEvent(Object messageData, CardsGameClass cardsGameClass);
    }
}