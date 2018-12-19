using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform m_target;

    private float m_secondsToQuery = 0.0f;
    private NavMeshAgent m_Agent;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_secondsToQuery -= Time.deltaTime;
        if (m_secondsToQuery <= 0.0f)
        {
            m_secondsToQuery = 1.0f;
            m_Agent.destination = m_target.position;
        }
    }
}
