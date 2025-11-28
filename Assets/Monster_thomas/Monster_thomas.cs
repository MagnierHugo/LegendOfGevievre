using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [field: SerializeField] public int Pv { get; protected set; } = 10;
    [field: SerializeField] public int Atk { get; protected set; } = 5;
    [field: SerializeField] public int Vit { get; protected set; } = 5;

    public virtual void Init(int pv, int atk, int vit)
    {
        Pv = pv;
        Atk = atk;
        Vit = vit;
    }

    public void PrendreDegats(int atk)
    {
        Pv -= atk;
    }
}
