using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour Menu/Ceate New Parkour Actions")]
public class NewParkourActions : ScriptableObject
{
    [SerializeField] string animationName;
    [SerializeField] float mininumHeight;
    [SerializeField] float maxinumHeight;


    public bool CheckIfAvailable(ObstacleInfo hitInfo, Transform player)
    {
        float checkHeight= hitInfo.heightInfo.point.y - player.position.y;

        if (checkHeight < mininumHeight || checkHeight > maxinumHeight)
            return false;
        return true;
    }

    public string AnimationName => animationName;
}
