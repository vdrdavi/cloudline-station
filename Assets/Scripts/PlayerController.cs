using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public float speed = 5f;

    private List<PathNode> currentPath;
    private int targetIndex;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, Camera.main.nearClipPlane));
            mousePos.z = 0f;

            currentPath = pathfinding.FindPath(transform.position, mousePos);
            targetIndex = 0;
        }

        if (currentPath != null && currentPath.Count > 0)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        if (targetIndex >= currentPath.Count)
        {
            currentPath = null;
            return;
        }

        Vector3 targetPosition = currentPath[targetIndex].worldPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetIndex++;
        }
    }
}