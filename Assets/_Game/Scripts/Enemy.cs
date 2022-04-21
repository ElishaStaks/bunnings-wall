using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform m_TargetPosition;

    private Transform m_Player;
    private NavMeshAgent m_Agent;
    private ActionPoint m_ActionPoint;
    private Animator m_Anim;
    private Vector3 m_LocalMovement;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        m_Agent = GetComponent<NavMeshAgent>();
        m_Player = Camera.main.transform;
        m_Anim = GetComponentInChildren<Animator>();

        // Ignore rotation
        m_Agent.updateRotation = false;
        m_Agent.updatePosition = true;
    }

    public void Initialise(ActionPoint point)
    {
        gameObject.SetActive(true);
        m_ActionPoint = point;

        if (m_Agent != null)
        {
            m_Agent.SetDestination(m_TargetPosition.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Anim.GetBool("Is Dead"))
        {
            LookAtTarget();
        }
        UpdateAnimBlend();
    }

    private void UpdateAnimBlend()
    {
        if (m_Anim != null || !m_Anim.enabled) return;

        if (m_Agent.remainingDistance > 0.01f)
        {
            if (m_Agent.remainingDistance > 0.01f)
            {
                m_LocalMovement = Vector3.Lerp(m_LocalMovement, transform.InverseTransformDirection(m_Agent.velocity).normalized, 2f * Time.deltaTime);

                m_Agent.nextPosition = transform.position;
            }
            else
            {
                m_LocalMovement = Vector3.Lerp(m_LocalMovement, Vector3.zero, 2f * Time.deltaTime);
            }

        } 
            m_Anim.SetFloat("X Speed", m_LocalMovement.x);
            m_Anim.SetFloat("Z Speed", m_LocalMovement.z);
    }

    private void LookAtTarget()
    {
        if (m_Player == null)
        {
            Debug.LogError("No player exists in the world! Please add one.");
            return;
        }

        Vector3 direction = m_Player.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void KillSelf()
    {
        GetCurrentActivePoint().EnemyKilled();
    }

    public void TriggerDeadAnim()
    {
        m_Anim.SetTrigger("Dead");
        m_Anim.SetBool("Is Dead", true);
    }
    public void TriggerDamagedAnim()
    {
        m_Anim.SetTrigger("Damaged");
    }

    public ActionPoint GetCurrentActivePoint()
    {
        return m_ActionPoint;
    }
}
