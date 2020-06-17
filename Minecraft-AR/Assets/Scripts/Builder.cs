using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class Builder : MonoBehaviour
{
    [SerializeField]
    private GameObject[] arrayOfBlocks;
    private ARRaycastManager raycastManager;// 
    // public GameObject cube;
    [SerializeField] 
    private LayerMask blockLayer;
    public int selectedBlock;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }
    public void OnBuildButtonPressed()
    {
        List<ARRaycastHit> arHits = new List<ARRaycastHit>(); // hits result
     
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));
        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Vector3 buildablePos = hitInfo.normal + hitInfo.transform.position;
            Quaternion buidableRot = hitInfo.transform.rotation;
            // whith direction hitted object looking
            Build(buildablePos, buidableRot);
        }
        else {   
         raycastManager.Raycast(rayToCast, arHits,TrackableType.Planes);
        //TrackableType.Planes -track all planes you detect
        // care about planes you hits
            if (arHits.Count > 0)
            {
                Vector3 builderPos = new Vector3(Mathf.Round(arHits[0].pose.position.x / 1) * 1, Mathf.Round(arHits[0].pose.position.y / 1) * 1, Mathf.Round(arHits[0].pose.position.z / 1) * 1);
                Quaternion buildablRotation = arHits[0].pose.rotation;
                Build(builderPos, buildablRotation);
            }
        }
    }
    void Build (Vector3 pos, Quaternion rot)
    {
        Instantiate(arrayOfBlocks[selectedBlock], pos, rot);
    }
    public void SelectBlock(int blockID)
    {
        selectedBlock = blockID;
    }
    public void DestroyBlock()
    {
        RaycastHit hitInfo;
        Ray rayToCast = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));
        if (Physics.Raycast(rayToCast, out hitInfo, 200f, blockLayer))
        {
            Destroy(hitInfo.collider.gameObject);
        }
    }
    public void DestroyAllBlocks()
    {
        GameObject[] tableOfBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject cell in tableOfBlocks)
        {
            Destroy(cell);
        }
    }

}
