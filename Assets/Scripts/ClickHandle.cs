using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandle : MonoBehaviour
{
    private AllyStonesHandle ASH;
    private BoardScript BS;
    [SerializeField] GameObject allyStonesObj;
    [SerializeField] GameObject boardObj;
    private void Start()
    {
        ASH = allyStonesObj.GetComponent<AllyStonesHandle>();
        BS = boardObj.GetComponent<BoardScript>();
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            bool castRay = false;
            Ray raycast = new Ray();
            if (Application.platform == RuntimePlatform.Android && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                castRay = true;
            }
            else if ((Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer) && Input.GetMouseButtonDown(0))
            {
                raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
                castRay = true;
            }
            if (castRay)
            {
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.transform.gameObject.tag == "AllyStone")
                    {
                        ASH.SelectStone((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.z, raycastHit.transform.gameObject);
                    }
                    if (raycastHit.transform.gameObject.tag == "KingAllyStone")
                    {
                        ASH.SelectKingStone((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.z, raycastHit.transform.gameObject);
                    }
                    if (raycastHit.transform.gameObject.tag == "Cell")
                    {
                        if (BS.GetCell((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.z).GetComponent<SlotScript>().IsSelected())
                        {
                            StartCoroutine(BS.MoveSelectedStone((int)raycastHit.transform.position.x, (int)raycastHit.transform.position.z));
                        }
                    }
                }
            }
        }
    }
}
