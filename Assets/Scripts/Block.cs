using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Range(1f, 20f)]
    public float VerticalSpeed;

    [HideInInspector]
    public BlockSpawner BlockSpawner;

    private Rigidbody m_rigidbody;
    private bool m_canRotate;
    private bool m_canGoLeft;
    private bool m_canGoRight;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_canRotate = true;
        m_canGoRight = true;
        m_canGoLeft = true;
        m_rigidbody.velocity = new Vector3(0f, -VerticalSpeed, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).normal == Vector3.up && !BlockSpawner.PlacedBlocks.Contains(this))
        {
            m_canRotate = false;
            m_canGoRight = false;
            m_canGoLeft = false;
            m_rigidbody.isKinematic = true;
            m_rigidbody.velocity = Vector3.zero;
            BlockSpawner.SpawnBlock = true;
            BlockSpawner.PlacedBlocks.Add(this);
            ActivateLineChecker();
            this.enabled = false;
        }
        if (collision.GetContact(0).normal == Vector3.right) m_canGoLeft = false;
        if (collision.GetContact(0).normal == -Vector3.right) m_canGoRight = false;

    }

    private void OnCollisionExit(Collision collision)
    {
        if (!m_canGoLeft) m_canGoLeft = true; 
        if (!m_canGoRight) m_canGoRight = true; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_canRotate) RotateBlock();
        if (Input.GetKeyDown(KeyCode.LeftArrow) && m_canGoLeft) TranslateBlock(new Vector3(-1f, 0f, 0f));
        if (Input.GetKeyDown(KeyCode.RightArrow) && m_canGoRight) TranslateBlock(new Vector3(1f, 0f, 0f));
        if (Input.GetKeyDown(KeyCode.DownArrow)) m_rigidbody.velocity = new Vector3(0f, -VerticalSpeed * 3f, 0f);
        if (Input.GetKeyUp(KeyCode.DownArrow)) m_rigidbody.velocity = new Vector3(0f, -VerticalSpeed, 0f);
    }

    private void RotateBlock()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f));
    }

    private void TranslateBlock(Vector3 direction)
    {
        transform.Translate(direction, Space.World);
    }

    private void ActivateLineChecker()
    {
        foreach(Transform child in transform)
        {
            if (child.TryGetComponent(out LineChecker lineChecker))
            {
                lineChecker.enabled = true;
            }
        }
    }
}
