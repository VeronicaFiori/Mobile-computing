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
    [SerializeField] bool lookAtObstacle;

    public Quaternion RequireRotation { get; set; }

    public bool CheckIfAvailable(ObstacleInfo hitData, Transform player)
    {
        float checkHeight= hitData.heightInfo.point.y - player.position.y;

        if (checkHeight < mininumHeight || checkHeight > maxinumHeight)
            return false;

        if (lookAtObstacle)
        {
            RequireRotation = Quaternion.LookRotation(hitData.hitInfo.normal);
        }

        return true;
    }

    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;
}
