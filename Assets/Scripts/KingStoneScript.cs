using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingStoneScript : MonoBehaviour
{
    private AllyStonesHandle WSH;
    private Material commonMaterial;
    private void Start()
    {
        commonMaterial = this.GetComponent<Renderer>().material;
        WSH = this.transform.GetComponentInParent<AllyStonesHandle>();
    }
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.gameObject == this.gameObject)
                    WSH.SelectKingStone((int)this.transform.position.x, (int)this.transform.position.z, this.gameObject);
            }
        }
    }
    private void OnMouseDown()
    {
        WSH.SelectKingStone((int)this.transform.position.x, (int)this.transform.position.z, this.gameObject);
    }
}
