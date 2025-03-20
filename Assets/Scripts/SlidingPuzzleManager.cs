using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class SlidingPuzzleManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private XRRayInteractor rayInteractorLeft;
    [SerializeField] private XRRayInteractor rayInteractorRight;
    [SerializeField] private InputActionProperty leftBumper;
    [SerializeField] private InputActionProperty rightBumper;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;
    private int temp;

    private void CreateGamePieces(float gapThickness) {
        //This is the width of each tile
        float width = 1 / (float)size;
        //creating the puzzle pieces based on the size
        for(int row = 0; row < size; row++) {
            for(int col = 0; col < size; col++) {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                //Pieces will be in a game board going from -1 to +1
                piece.localPosition = new Vector3(-1 + (2*width*col) + width, 
                +1-(2*width*row)-width,
                0);
                piece.localScale = ((2*width)-gapThickness) * Vector3.one;
                piece.name = $"{(row*size) + col}";
                //we want an empty space in the bottom right
                if((row == size -1) && (col == size -1)) {
                    emptyLocation = (size * size) -1;
                    piece.gameObject.SetActive(false);
                } else {
                    //We want to map the UV coordinates appropriately, they are 0->1
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    //UV coord order: (0,1), (1,1), (0,0), (1,0)
                    uv[0] = new Vector2((width * col) + gap, 1-((width * (row +1)) -gap));
                    uv[1] = new Vector2((width *(col +1)) - gap, 1 - ((width * (row + 1)) -gap));
                    uv[2] = new Vector2((width * col) + gap, 1- ((width*row) + gap));
                    uv[3] = new Vector2((width *(col +1)) - gap, 1- ((width*row) + gap));
                    //Assign our new UVs to the mesh
                    mesh.uv = uv;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        temp = 0;
    }

    //Update is called once per frame
    void Update() {
        //check for completion
        if(!shuffling && CheckCompletion()) {
            if(temp == 0) {
                shuffling = true;
                StartCoroutine(WaitShuffle(0.5f));
                temp = 1;
            }else{
                Debug.Log("Beat the minigame");
            }
        }
        //need to put the VR checking for when the ray hits and user presses bumper
        if(leftBumper.action.WasPressedThisFrame()) {
            //need to get where the ray hits when the user presses the bumper
            RaycastHit hit;
            if(rayInteractorLeft.TryGetCurrent3DRaycastHit(out hit)) {
                // Go through the list, the index tells us the position
                for(int i = 0; i < pieces.Count; i++) {
                    if(pieces[i] == hit.transform) {
                        //check each direction to see if valid move
                        //We break out on success so we don't carry on and swap back again
                        if(SwapIfValid(i, -size, size)) { break; }
                        if(SwapIfValid(i, +size, size)) { break; }
                        if(SwapIfValid(i, -1, 0 )) { break; }
                        if(SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        } else if(rightBumper.action.WasPressedThisFrame()) {
            //need to get where the ray hits when the user presses the bumper
            RaycastHit hit;
            if(rayInteractorRight.TryGetCurrent3DRaycastHit(out hit)) {
                // Go through the list, the index tells us the position
                for(int i = 0; i < pieces.Count; i++) {
                    if(pieces[i] == hit.transform) {
                        //check each direction to see if valid move
                        //We break out on success so we don't carry on and swap back again
                        if(SwapIfValid(i, -size, size)) { break; }
                        if(SwapIfValid(i, +size, size)) { break; }
                        if(SwapIfValid(i, -1, 0 )) { break; }
                        if(SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck) {
        if(((i % size ) != colCheck) && ((i + offset) == emptyLocation)) {
            //Swap them in game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            //Swap their transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            //update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }

    //We name the pieces in order so we can use this to check completion
    private bool CheckCompletion() {
        for(int i = 0; i < pieces.Count; i++) {
            if(pieces[i].name != $"{i}") {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration) {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    //Brute force shuffling
    private void Shuffle() {
        int count = 0;
        int last = 0;
        while(count < (size * size * size)) {
            //pick a random location
            int rnd = Random.Range(0,size*size);
            //Only thing we forbid is undoing the last move
            if(rnd == last) { continue; }
            last = emptyLocation;
            //Try surrounding spaces looking for valid move
            if(SwapIfValid(rnd, -size, size)) {
                count++;
            } else if (SwapIfValid(rnd, +size, size)) {
                count++;
            }else if(SwapIfValid(rnd, -1, 0)) {
                count++;
            }else if(SwapIfValid(rnd, +1, size -1)) {
                count++;
            }
        }
    }
}
