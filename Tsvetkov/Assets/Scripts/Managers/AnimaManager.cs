using UnityEngine;
using InterfaceNamespace;

public class AnimaManager : IAnimaManager //Наследуем класс и реализуем интерфейс.
{ 
    public void SetStateAnima(GameObject obj, string name, bool flag)
    {
        obj.GetComponent<Animator>().SetBool(name, flag);
    }
    
    public void SetStateAnima(GameObject obj, string name, int i)
    {
        obj.GetComponent<Animator>().SetInteger(name, i);
    }
}
