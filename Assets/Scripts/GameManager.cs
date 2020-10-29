using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        SharedObjects.eventSystem = FindObjectOfType<EventSystem>();
        SharedObjects.mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Animator.Update();
    }
}