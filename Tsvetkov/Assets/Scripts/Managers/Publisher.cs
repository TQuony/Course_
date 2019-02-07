
using System.Collections.Generic;
using InterfaceNamespace;
using UnityEngine;

public class Publisher : IPublisher
{

    private readonly IList<ISubscriber> _subscribers = new List<ISubscriber>();
        
    public void Publish(object sender, object messagedata)
    {
        foreach (var _subscriber in _subscribers)
        {
            _subscriber.OnEvent(messagedata);
        }
    }
    public void Publish(object sender, object messagedata, CardsGameClass cardsGameClass)
    {
        foreach (var _subscriber in _subscribers)
        {
            _subscriber.OnEvent(messagedata, cardsGameClass);
        }
    }
    public void AddSubscriber(ISubscriber subscriber)
    {
        if (_subscribers.Contains(subscriber) == false)
        {
            _subscribers.Add(subscriber);
        }
    }
}
