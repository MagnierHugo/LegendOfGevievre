using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected int pos_x;
    protected int pos_y;
    [SerializeField] protected int pv = 10;
    [SerializeField] protected int atk = 5;
    [SerializeField] protected int vit = 5;
    public int getpv()
    {
        return pv;
    }
    public int getatk()
    {
        return atk;
    }
    public int getvit()
    {
        return vit;
    }

    public void degat_recu(int atk)
    {
        pv -= atk;
    }
}
