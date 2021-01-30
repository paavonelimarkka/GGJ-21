using System.Collections;
using UnityEngine;

// Is this needed? Just a regular string for now lol
public class AnimalItem
{
    
    private string itemName;
    
    public AnimalItem(string name) {
        itemName = name;
    }

    public override string ToString() {
        return itemName;
    }
}
