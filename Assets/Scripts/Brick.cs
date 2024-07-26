using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
  public float breakForce = 1000f;
  public float snapDistance = 0.5f; // 磁吸距离
  public float jointFrequency = 5.0f; // 关节的弹性频率
  public float jointDamping = 0.7f; // 关节的阻尼
  private Rigidbody2D rb;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (Application.isEditor)
    {
      CheckForSnap();
    }
  }

  void CheckForSnap()
  {
    Brick[] bricks = FindObjectsOfType<Brick>();
    foreach (Brick otherBrick in bricks)
    {
      if (otherBrick != this && Vector2.Distance(transform.position, otherBrick.transform.position) < snapDistance)
      {
        SnapToBrick(otherBrick);
        break;
      }
    }
  }

  void SnapToBrick(Brick otherBrick)
  {
    // 将砖块位置对齐
    transform.position = otherBrick.transform.position;

    // 添加关节
    HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
    joint.connectedBody = otherBrick.GetComponent<Rigidbody2D>();
    joint.breakForce = breakForce;
    joint.useLimits = true;
    joint.limits = new JointAngleLimits2D { min = -15, max = 15 };
    //joint.frequency = jointFrequency;
    //joint.dampingRatio = jointDamping;

    // 禁用拖拽功能
    //Destroy(GetComponent<LeanDragTranslate>());
  }
}