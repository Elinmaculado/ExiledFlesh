using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    [SerializeField] private int gridSize;
    
    bool isShuffling = false;


    private void Start() {
        pieces = new List<Transform>();
        CreateGamePieces();
        Shuffle();
    }

    private void CreateGamePieces(){
        float widht = 1/(float)gridSize;
        float pieceGap = 0.01f;
        for(int row=0; row<gridSize; row++){
            for(int col = 0; col<gridSize; col++){
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3((-1 + (2*widht*col)+widht)/2,//x
                                                0.6f,//y
                                                (1 - (2*widht*row) - widht)/2);//z
                piece.localScale = (widht-pieceGap)*Vector3.one;
                piece.name = $"{(row * gridSize)+col}";

                if(row == (gridSize - 1) && col == (gridSize - 1)){
                    emptyLocation = (row*gridSize)+ col;
                    Debug.Log(emptyLocation);
                    piece.gameObject.SetActive(false);
                }
                float gap = pieceGap/2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2((widht*col)+gap,1-(widht*(row+1)-gap));
                uv[1] = new Vector2((widht*(col+1))-gap,1-((widht*(row+1))-gap));
                uv[2] = new Vector2((widht*col)+gap,1-((widht*row)+gap));
                uv[3] = new Vector2((widht*(col+1))-gap,1-(widht*row)+gap);
                mesh.uv = uv;
            }
        }
        piecePrefab.gameObject.SetActive(false);
    }

    public void SwapPiece(Transform piece){
        if(isShuffling){return;}
        for(int i = 0; i<pieces.Count; i++){
            if(piece == pieces[i]){
                Debug.Log("Founded");
                if(IsSwapValid(i,-gridSize,gridSize)){break;}
                if(IsSwapValid(i, gridSize,gridSize)){break;}
                if(IsSwapValid(i,-1, 0)){break;}
                if(IsSwapValid(i, 1, gridSize - 1 )){break;}
            }
        }
    }

    public bool IsSwapValid(int i, int offset, int colCheck){
        if(((i%gridSize) != colCheck) && ((i+offset)==emptyLocation)){
            (pieces[i], pieces[i+offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i+offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            emptyLocation = i;
            if(CheckCompletion()){Debug.Log("Completed");}
            return true;
        }
        return false;
    }

    bool CheckCompletion(){
        for(int i = 0; i<pieces.Count; i++){
            if(pieces[i].name != $"{i}"){
                return false;
            }
        }
        return true;
    }

    private void Shuffle(){
        isShuffling = true;
        int count = 0;
        int last = 0;
        
        while(count<(gridSize * gridSize * gridSize)){
            int randomIndex = UnityEngine.Random.Range(0,(gridSize*gridSize)-1);
            if(randomIndex == last){continue;}
            last = emptyLocation;

            if(IsSwapValid(randomIndex,-gridSize,gridSize)){count++;}
            else if(IsSwapValid(randomIndex, gridSize,gridSize)){count++;}
            else if(IsSwapValid(randomIndex,-1, 0)){count++;}
            else if(IsSwapValid(randomIndex, 1, gridSize - 1 )){count++;}
            
        }
        isShuffling = false;
    }
}
