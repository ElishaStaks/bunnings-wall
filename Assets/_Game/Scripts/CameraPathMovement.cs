using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CameraPathMovement : MonoBehaviour
{
    [SerializeField]
    private PathCreator m_Path;

    [SerializeField]
    private EndOfPathInstruction m_EndOfPathInstruction;

    [SerializeField]
    private float m_Speed = 3f;

    [SerializeField]
    private bool m_IsMoving = false;

    [SerializeField]
    private ActionPointEntry[] m_ActionPointEntries;

    [Header("Debug Options")]
    [SerializeField]
    private float m_PreviewDistance = 0f;

    [SerializeField]
    private bool m_EnableDebug = false;

    private float m_DistanceTravelled;

    private void Start()
    {
        foreach (var entry in m_ActionPointEntries)
        {
            entry.actionPoint.Initialise(this);
        }

        SetCameraMovement(true);
    }

    private void Update()
    {
        if (m_Path != null && m_IsMoving)
        {
            m_DistanceTravelled += m_Speed * Time.deltaTime;
            transform.position = m_Path.path.GetPointAtDistance(m_DistanceTravelled, m_EndOfPathInstruction);
            transform.rotation = m_Path.path.GetRotationAtDistance(m_DistanceTravelled, m_EndOfPathInstruction);

            for (int i = 0; i < m_ActionPointEntries.Length; i++)
            {
                if ((m_Path.path.GetPointAtDistance(m_ActionPointEntries[i].distance) - transform.position).sqrMagnitude < 0.001f)
                {
                    if (m_ActionPointEntries[i].actionPoint.AreaCleared)
                    {
                        return;
                    }

                    if (m_IsMoving)
                    {
                        m_ActionPointEntries[i].actionPoint.CameraReachedPoint();
                    }
                }
            }
        }
    }

    public void SetCameraMovement(bool isEnable)
    {
        m_IsMoving = isEnable;
    }

    /// <summary>
    /// Whenever we change a value in inspector this method gets executed.
    /// </summary>
    private void OnValidate()
    {
        if (m_EnableDebug)
        {
            transform.position = m_Path.path.GetPointAtDistance(m_PreviewDistance, m_EndOfPathInstruction);
            transform.rotation = m_Path.path.GetRotationAtDistance(m_PreviewDistance, m_EndOfPathInstruction);
        }
    }
}

 [System.Serializable]
 public class ActionPointEntry
{
    public ActionPoint actionPoint;
    public float distance;
}
