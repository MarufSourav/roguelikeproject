using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    public string playername;
    public int gunID;
    public int health;
    public int speed;
    public int deathcounter;    
}