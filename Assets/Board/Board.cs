using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    List<BoardItem> boardItems = new List<BoardItem>();
    public float width = 1f;
    public float padding = 0.5f;
    public float radius = 20f;

    public BoardItem focusTarget;
    public float focusOffset = 0.5f;

    private BoardItem dragTarget;
    private Vector3 dragTargetPosition;

    public BoxCollider2D boardArea;

    public enum LayoutType {Horizontal, Radial};
    public LayoutType layoutType = LayoutType.Horizontal;

    void Awake()
    {
        boardArea = GetComponent<BoxCollider2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        int targetIndex = boardItems.IndexOf(focusTarget);
        switch(layoutType)
        {
            case LayoutType.Horizontal: HorizontalLayout(targetIndex); break;
            case LayoutType.Radial: RadialLayout(targetIndex); break;
        }

        if (dragTarget) dragTarget.position = dragTargetPosition;

        focusTarget = null;
        dragTarget = null;
    }

    void RadialLayout(int targetIndex = -1)
    {
        for(int i = 0; i < boardItems.Count; i++)
        {
            float angleOffset = 0f;
            float radiusOffset = 0f;

            if (targetIndex != -1)
            {
                if (i == targetIndex - 1) angleOffset = -focusOffset;
                else if (i == targetIndex + 1) angleOffset = focusOffset;
                else if (i == targetIndex) radiusOffset = focusOffset;
            }

            boardItems[i].position = RadialPosition(i, angleOffset, radiusOffset);
            boardItems[i].transform.rotation = RadialRotation(boardItems[i].position);
        }
    }

    Vector3 RadialPosition(int index, float angleOffset, float radiusOffset)
    {
        Vector3 origin = transform.position + Vector3.down * radius;
        float circ = Mathf.PI * 2f * radius;
        int count = boardItems.Count;
        float totalWidth = (count * width) + ((count - 1) * padding);
        float totalAngle = (totalWidth / circ) * 360f;
        float angle = totalAngle / count;
        float startAngle = -(totalAngle / 2);
        Vector3 startDirection = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.up;
        //startDirection = Quaternion.AngleAxis(((float)index + 0.5f) * (angle + angleOffset), Vector3.forward) * startDirection;
        startDirection = Quaternion.AngleAxis(((float)index + 0.5f) * angle + angleOffset, Vector3.forward) * startDirection;
        return origin + startDirection * (radius + radiusOffset);
    }

    Quaternion RadialRotation(Vector3 position)
    {
        Vector3 origin = transform.position + Vector3.down * radius; 
        return Quaternion.LookRotation(Vector3.forward, (position - origin));
    }

    void HorizontalLayout(int targetIndex = -1){
        int count = boardItems.Count;
        float totalWidth = (count * width) + (count - 1) * padding;
        Vector3 start = transform.position + Vector3.left * (totalWidth / 2);

        for(int i = 0; i < count; i++)
        {
            float index = (float)i + 0.5f;
            Vector3 position = start + Vector3.right * ((index * width) + (i * padding));
            boardItems[i].position = position;

            if (targetIndex != -1)
            {
                if (i == targetIndex - 1) position += Vector3.left * focusOffset;
                else if (i == targetIndex + 1) position += Vector3.right * focusOffset;
                else if (i == targetIndex) position += Vector3.up * focusOffset;
            }

            boardItems[i].position = position;
        }
    }

    public GameObject NewBoardItem(GameObject prefab)
    {
        GameObject newBoardItem = Instantiate(prefab);
        newBoardItem.transform.parent = transform;
        BoardItem boardItem = newBoardItem.GetComponent<BoardItem>();
        boardItems.Add(boardItem);
        return newBoardItem;
    }

    public void DestroyBoardItem(BoardItem boardItem)
    {
        if(!boardItems.Contains(boardItem)) return;
        boardItems.Remove(boardItem);
        Destroy(boardItem.gameObject);
    }

    public BoardItem GetNearestBoardItem(Vector3 position)
    {
        if (!InBoardArea(position)) return null;
        BoardItem target = null;
        float minDistance = 1000f;

        foreach(BoardItem b in boardItems)
        {
            float distance = (position - b.transform.position).magnitude;
            if (distance <= minDistance)
            {
                minDistance = distance;
                target = b;
            }
        }
        return target;
    }

    public bool InBoardArea(Vector3 position)
    {
        return boardArea.OverlapPoint(position);
    }

    public void SetDragTarget(BoardItem dragTarget, Vector3 dragTargetPosition)
    {
        this.dragTarget = dragTarget;
        this.dragTargetPosition = dragTargetPosition;
    }
}
