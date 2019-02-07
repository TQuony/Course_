using UnityEngine;
namespace InterfaceNamespace
{
    public interface IAnimaManager
    {
        void SetStateAnima(GameObject obj, string name, bool flag);
        void SetStateAnima(GameObject obj, string name, int num);
    }
}