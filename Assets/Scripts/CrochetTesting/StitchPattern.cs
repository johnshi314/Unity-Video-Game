using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StitchPattern : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float snapRange = 1f;


    private bool isDragging, _placed;
    private Vector2 offset, originalPos;

    private StitchSlot _slot;
    

    public void Init(StitchSlot slot)
    {
        _renderer.sprite = slot.Renderer.sprite;
        _slot = slot;
    }

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        if (_placed) return;
        if (!isDragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
        
    }

    private void OnMouseDown()
    {
        isDragging = true;       
        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if(Vector2.Distance(transform.position,_slot.transform.position) < snapRange)
        {
            transform.position = _slot.transform.position;
            _placed = true;
        } else
        {
            transform.position = originalPos;
            isDragging = false;
        }
        
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
