using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckerBoard : MonoBehaviour
{
    public Piece[,] pieces = new Piece[8, 8];
    public Square[,] squres = new Square[8, 8];
   
    public Vector2 mouseOver;

    public Piece selectedPiece;

    private Vector2 StartDrag;
    private Vector2 EndDrag;
    private bool isWhiteTurn;
    public bool isWhite;
    public CanvasGroup alertCanvas;
    public CanvasGroup checkScoreW;
    public CanvasGroup checkScoreB;
    private float lastAlert;
    public bool alertisActive;
    public GameObject highligt;
    private int wS;
    private int bS;

    public Vector3 boardoffset = new Vector3(-0.5f, 0f, -0.5f); 

    private void Start()
    {
        AlertVictory(":JGFt");
        wS = 0;
        bS = 0;
        isWhiteTurn = true;
        isWhite = true;
        GenerateBoard();
        GeneratePiecesOnBoard();
        highligt = Instantiate(Resources.Load("selectHighlight", typeof(GameObject))) as GameObject;
    }
    private void Update()
    {
        UpdateAlert();
        CheckScore();
        Highlighter();
        if ((isWhite) ? isWhiteTurn : !isWhiteTurn)
        {
            UpdateMouseOver();
            Debug.Log(mouseOver);
            int x = (int)mouseOver.x;
            int y = (int)mouseOver.y;

           
            if (Input.GetMouseButtonDown(0) && (selectedPiece == null || (selectedPiece != null && pieces[x, y] != null)))
            {

                    SelectPiece(x, y);

                    return;

                
            }
            if (Input.GetMouseButtonDown(0) && selectedPiece != null)
            {
               
                TryMove((int)StartDrag.x, (int)StartDrag.y, x, y);
                
            }
            

        }
       

    }

    private void UpdateMouseOver()
    {
        if (!Camera.main)
        {
            Debug.Log("Unable to find camera");
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 30.0f, LayerMask.GetMask("Board")))
        {
            mouseOver.x = (int)(hit.point.x-boardoffset.x);
            mouseOver.y = (int)(hit.point.z-boardoffset.z);
        }
        else
        {
            mouseOver.x = -1;
            mouseOver.y = -1;
        }

        

    }
 
    private void SelectPiece(int x, int y)
    {
        
        if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
            return;
        Piece p = pieces[x, y];
        if (p != null&&(p.isWhite==isWhite))
        {
            selectedPiece = p;
            StartDrag = mouseOver;
            Debug.Log(selectedPiece.name);
            
        }
        else return;
    }
    
    private void TryMove(int x1, int y1, int x2, int y2)
    {
        StartDrag = new Vector2(x1, y1);
        EndDrag = new Vector2(x2, y2);
        selectedPiece = pieces[x1, y1];
        
        if (x2 < 0 || x2 >= pieces.Length || y2 < 0 || y2 >= pieces.Length)
        {
            if (selectedPiece != null)
                MovePiece(selectedPiece, x1, y1);

            StartDrag = Vector2.zero;
            selectedPiece = null;
            return;
        }
        if(selectedPiece != null)
        {
            if (EndDrag == StartDrag|| !selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
            {
               
                return;
            }
            if(selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
            {
                pieces[x2, y2] = selectedPiece;
                pieces[x1, y1] = null;
                MovePiece(selectedPiece, x2, y2);
                EndTurn();
            }
            

        }
        

    }
    public void EndTurn()
    {
        selectedPiece = null;
        StartDrag = Vector2.zero;
        isWhiteTurn = !isWhiteTurn;
        isWhite = !isWhite;
        CheckerBoardScore(isWhite);
        CheckVictory();
    }

    private void CheckerBoardScore(bool isWho)
    {
        if (!isWho) wS++;
        else bS++;      

    }
    private void CheckScore()
    {
        checkScoreW.GetComponentInChildren<Text>().text = Convert.ToString(wS);
        checkScoreB.GetComponentInChildren<Text>().text = Convert.ToString(bS);
    }

    private void CheckVictory()
    {
        int CheckWhiteWin = 0;
        int CheckBlackWin = 0;
        for (int y = 7; y > 4; y--)
        {
            for (int x = 7; x >4; x--)
            {
                if (pieces[x, y] != null)
                {
                    if (pieces[x, y].isWhite == true)
                    {
                        CheckWhiteWin++;
                        if (CheckWhiteWin == 8)
                        {
                            AlertVictory("White Player is Win!");
                            GameManager.Instance.StartMenu();
                        }

                    }
                }
            }
        }
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3 ; x++)
            {

                if(pieces[x, y] != null) {
                    if (pieces[x, y].isWhite == false)
                    {
                        CheckBlackWin++;
                        if (CheckWhiteWin == 8)
                        {

                            AlertVictory("Black Player is Win!");
                            GameManager.Instance.StartMenu();
                        }

                    }
                }
            }
        }
    }
    private void Highlighter()
    {
        if(selectedPiece != null)
            highligt.transform.position = selectedPiece.transform.position + Vector3.up * 0.1f;
            highligt.SetActive(true);
        if(selectedPiece==null)
        {
            highligt.SetActive(false);
        }
    }
    public void GenerateBoard()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                GenerateSquare(x, y);
            }
        }
        
    }

    public void GenerateSquare(int x, int y)
    {
        bool isSquareWhite = ((y + x) % 2!=0) ? false : true;
        GameObject go = Instantiate((isSquareWhite) ? (Resources.Load("WhiteSquare", typeof(GameObject))) : (Resources.Load("BlackSquare", typeof(GameObject)))) as GameObject;
        go.transform.position = new Vector3(x, 0, y);
        go.transform.SetParent(transform);
        Square s = go.GetComponent<Square>();
        squres[x, y] = s;


    }

    public void GeneratePiecesOnBoard()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                GeneratePieces(x,y);
            }
        }
        for (int y = 7; y > 4; y--)
        {
            for (int x = 7; x > 4; x--)
            {
                GeneratePieces(x, y);
            }
        }
    }

    private void GeneratePieces(int x, int y)
    {
        bool isPieceWhite = (y > 3) ? false : true;
        GameObject go =  Instantiate( (isPieceWhite) ? (Resources.Load("WhitePiece", typeof(GameObject))) : (Resources.Load("BlackPiece", typeof(GameObject)))) as GameObject;
        go.transform.SetParent(transform);
        Piece p = go.GetComponent<Piece>();
        pieces[x, y] = p;
        MovePiece(p, x, y);
    }
    private void MovePiece(Piece p, int x, int y)
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y)+(Vector3.up*0.5f);
    }

    private void AlertVictory(string Text)
    {
        alertCanvas.GetComponentInChildren<Text>().text = Text;
        lastAlert = Time.time;
        alertCanvas.alpha = 1;
        alertisActive = true;
    }
    public void UpdateAlert()
    {
        if (alertisActive)
        {
            alertCanvas.alpha =(5 - (Time.time - lastAlert));

            if ((Time.time - lastAlert) > 10.0f)
            {
                alertisActive = false;
            }
        }
    }
}

