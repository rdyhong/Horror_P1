using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string _name;
    public string Name() => _name;
    public void GetItem()
    {
        
    }
}
