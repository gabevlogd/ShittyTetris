using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    private void Awake() => this.enabled = false;

    private void OnEnable()
    {
        CheckLine();
        this.enabled = false;
    }

    private void CheckLine()
    {
        RaycastHit[] raycastHitsRight = Physics.RaycastAll(transform.position, Vector3.right, 22f);
        RaycastHit[] raycastHitsLeft = Physics.RaycastAll(transform.position, -Vector3.right, 22f);
        if (raycastHitsLeft.Length + raycastHitsRight.Length >= 16)
        {
            Debug.Log("Line complete");

            foreach (RaycastHit raycastHit in raycastHitsRight)
            {
                if (raycastHit.collider.gameObject.tag != "MapEdge") Destroy(raycastHit.collider.gameObject);
            }
            foreach (RaycastHit raycastHit in raycastHitsLeft)
            {
                if (raycastHit.collider.gameObject.tag != "MapEdge") Destroy(raycastHit.collider.gameObject);
            }
            ReplaceBlocks();
            Destroy(this.gameObject);
        }
    }

    private void ReplaceBlocks()
    {
        Debug.Log(BlockSpawner.PlacedBlocks.Count);
        foreach(Block block in BlockSpawner.PlacedBlocks)
        {
            //block.transform.Translate(Vector3.down, Space.World);
            block.GetComponent<Rigidbody>().isKinematic = false;
            block.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
