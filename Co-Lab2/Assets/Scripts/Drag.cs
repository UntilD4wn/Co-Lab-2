using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private float zCoord;
    public GameObject picnicEndUI;

    [SerializeField] private Transform targetPosition;  // Assign the correct drop position in the Inspector
    [SerializeField] private float snapThreshold = 0.5f; // Distance threshold for snapping
    private bool isCorrectlyPlaced = false;

    private void Start()
    {
        picnicEndUI.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isCorrectlyPlaced) return;  // Prevent moving if already placed correctly

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 mousePosition = GetMouseWorldPos();
        offset = transform.position - mousePosition;
        dragging = true;
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;

        // Check if object is close to the target position
        if (Vector2.Distance(transform.position, targetPosition.position) < snapThreshold)
        {
            transform.position = targetPosition.position; // Snap to the correct place
            isCorrectlyPlaced = true;  // Mark as placed correctly
            CheckGameCompletion(); // Check if all objects are placed
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void CheckGameCompletion()
    {
        // Find all draggable objects in the scene
        Drag[] allDraggableObjects = FindObjectsOfType<Drag>();

        foreach (Drag obj in allDraggableObjects)
        {
            if (!obj.isCorrectlyPlaced)
                return;  // If any object is not placed correctly, exit early
        }

        picnicEndUI.SetActive(true);
    }
}
