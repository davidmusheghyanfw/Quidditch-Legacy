using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;

    public UnityAction<Vector3, Vector3> OnTouchDrag;
    public UnityAction<Vector3> OnTouchUp;
    public UnityAction<Vector3> OnTouchDown;

    public bool TouchActive { get; set; }
    public Vector3 DeltaPosition
    {
        get { return m_DeltaPosition; }
    }

    Vector3 m_StartPosition;
    Vector3 m_CurrentPosition;
    Vector3 m_LastPosition;
    Vector3 m_DeltaPosition;
    Vector3 m_TotalDeltaPosition;

    public bool isTouchDown = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))// && !IsPointerOverUI() && GameManager.instance.isGameStarted)
        {
            m_StartPosition = Input.mousePosition;
            m_LastPosition = m_StartPosition;
            m_DeltaPosition = Vector3.zero;

            OnTouchDown?.Invoke(m_StartPosition);

            isTouchDown = true;

            //FingerController.instance.Push();
        }
        if (Input.GetMouseButton(0))// && !IsPointerOverUI() && GameManager.instance.isGameStarted)
        {
            m_CurrentPosition = Input.mousePosition;
            m_DeltaPosition = m_CurrentPosition - m_LastPosition;
            m_LastPosition = m_CurrentPosition;
            m_TotalDeltaPosition = m_CurrentPosition - m_StartPosition;

           
            OnTouchDrag?.Invoke(m_CurrentPosition, m_TotalDeltaPosition);
        }
        if (Input.GetMouseButtonUp(0))// && !IsPointerOverUI() && GameManager.instance.isGameStarted)
        {
            m_DeltaPosition = Vector3.zero;

            OnTouchUp?.Invoke(m_DeltaPosition);

            isTouchDown = false;

            //FingerController.instance.Release();
        }
    }

    //bool ispointeroverui()
    //{
    //    pointereventdata eventdatacurrentposition = new pointereventdata(eventsystem.current);
    //    eventdatacurrentposition.position = input.mouseposition;

    //    list<raycastresult> results = new list<raycastresult>();
    //    eventsystem.current.raycastall(eventdatacurrentposition, results);

    //    for(int i = 0; i < results.count; i++)
    //    {
    //        if (results[i].gameobject.layer != layermask.nametolayer("ui") || results[i].gameobject.getcomponent<ignoreuiprevention>())
    //        {
    //            results.removeat(i);
    //            i--;
    //        }
    //    }

    //    return results.count > 0;
    //}
}
