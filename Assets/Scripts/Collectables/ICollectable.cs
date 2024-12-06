using UnityEngine;

public interface ICollectable
{
    public GameData gameData { set; get;}
    public void Collect();
}