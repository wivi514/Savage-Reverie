using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimDownSight : MonoBehaviour
{
    private CinemachineVirtualCamera m_Camera;

    private void Awake()
    {
        m_Camera = GameObject.Find("FPS Camera").GetComponent<CinemachineVirtualCamera>();
    }

    public void Aiming(bool isAiming)
    {
        if (isAiming)
        {
            m_Camera.m_Lens.FieldOfView = 40f;
        }
        if(!isAiming)
        {
            m_Camera.m_Lens.FieldOfView = 60f;
        }
    }
}
