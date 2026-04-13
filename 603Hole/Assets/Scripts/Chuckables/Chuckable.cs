using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Chuckable", menuName = "Scriptable Objects/Chuckable")]
public class Chuckable : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public int HFU;
    public int GoldReq;
    public int WoodReq;
    public int WaterReq;
    public int StoneReq;
    public int GoopReq;

    public bool ValidateBuyable(float gold, float wood, float water, float stone, float goop)
    {
        return gold >= GoldReq && wood >= WoodReq && water >= WaterReq && stone >= StoneReq && goop >= GoopReq;
    }
}
