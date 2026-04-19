
using UnityEngine;

[System.Serializable]
public enum EnumTag
{
    Player,
    Enemy,
    Bullet,
    Default
}

public class EnumTagComponent : MonoBehaviour
{
    public EnumTag tagValue =  EnumTag.Default;
}
