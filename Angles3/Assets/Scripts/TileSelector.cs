
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    

    private GameObject tileHighlight;
    private GameObject trueMoveHighlight;
    public Vector2 gridpoint;
    CheckerBoard checkerBoard;
    public bool isFind;

    void Start()
    {
        Vector3 point = new Vector3(0,0.52f,0);
        gridpoint.x = (int)point.x;
        gridpoint.y= (int)point.z;
        tileHighlight = Instantiate((Resources.Load("tileHighlight", typeof(GameObject)) as GameObject),point, Quaternion.identity, gameObject.transform);
        trueMoveHighlight= Instantiate((Resources.Load("selectHighlight", typeof(GameObject)) as GameObject), point, Quaternion.identity, gameObject.transform); ;
        tileHighlight.SetActive(false);
        trueMoveHighlight.SetActive(false);
        checkerBoard =GetComponent<CheckerBoard>();
        isFind=false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            Vector3 point = hit.point - checkerBoard.boardoffset;

            trueMoveHighlight.SetActive(false);
            tileHighlight.SetActive(true);
            tileHighlight.transform.position = new Vector3((int)point.x, 0.52f, (int)point.z);
            if (checkerBoard.selectedPiece != null)
            {
                if (checkerBoard.selectedPiece.ValidMove(checkerBoard.pieces, (int)checkerBoard.selectedPiece.transform.position.x, (int)checkerBoard.selectedPiece.transform.position.z, (int)point.x, (int)point.z))
                {
                    trueMoveHighlight.SetActive(true);
                    trueMoveHighlight.transform.position = new Vector3((int)point.x, 0.52f, (int)point.z);
                    tileHighlight.SetActive(false);
                }
            }
            if(checkerBoard.selectedPiece == null)
            {
                return;
            }
            else
            {
                return;
            }
        }
    }
       
}

