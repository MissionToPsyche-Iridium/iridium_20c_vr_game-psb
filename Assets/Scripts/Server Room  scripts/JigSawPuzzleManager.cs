using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
public class JigSawPuzzleManager : MonoBehaviour
{       
        [Header("Game Manager")]
        [Range(3,6)]
        [SerializeField] private int difficulty = 4;

        [Header("Jigsaw Puzzle")]
        [SerializeField] private List<Texture2D> jigsawPuzzleImages;
        [SerializeField] private Transform jigsawBoard;
        [SerializeField] private Transform pieceHolder;
        [SerializeField] private Transform piecePrefab;
        private List<Transform> jigsawPieces;
        private Vector2Int dimensions;
        private float height;
        private float width;
        private Transform draggedPiece;
        private Vector3 offset;
        [SerializeField] private XRRayInteractor rayInteractorLeft;
        [SerializeField] private XRRayInteractor rayInteractorRight;
        [SerializeField] private InputActionProperty leftBumper;
        [SerializeField] private InputActionProperty rightBumper;
        private int piecesCorrect = 0;

    // Start is called before the first frame update
    void Start()
    {
        startGame (jigsawPuzzleImages[0]);

    }

    public void startGame(Texture2D jigsawImage)
    {
        jigsawPieces = new List<Transform>();
        dimensions = getDimensions(jigsawImage, difficulty);
        createJigsawPieces(jigsawImage);
        // Shuffle the pieces and place them on the board
        scatter();

        updateBorder();
    }


    Vector2Int getDimensions(Texture2D jigsawImage, int difficulty)
    {
        Vector2Int dimensions = Vector2Int.zero;

        if (jigsawImage.width > jigsawImage.height)
        {
            dimensions.x = difficulty;
            dimensions.y = (difficulty * jigsawImage.height) / jigsawImage.width;
        }
        else if (jigsawImage.width < jigsawImage.height)
        {
            dimensions.x = (difficulty * jigsawImage.width) / jigsawImage.height;
            dimensions.y = difficulty;
        }
        return dimensions;
    } 
    void createJigsawPieces(Texture2D jigsawImage)
    {
        
        height = 1f / dimensions.y;
        float aspect = (float)jigsawImage.width / jigsawImage.height;
        width  = aspect / dimensions.x;

        for (int row = 0; row < dimensions.y; row++)
        {
            for (int column = 0; column < dimensions.x; column++)
            {
                Transform piece = Instantiate(piecePrefab, pieceHolder);
                piece.localPosition = new Vector3(
                    -width * dimensions.x / 2 + (width *
                     column), (-height * dimensions.y / 2) + (height * row), -1);
                     piece.localScale = new Vector3(width, height, 1);
                     piece.name = $"Piece {row*dimensions.x + column}"; ;
                jigsawPieces.Add(piece);

                float width1 = 1f / dimensions.x;
                float height1 = 1f / dimensions.y;

                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2(column * width1, row * height1);
                uv[1] = new Vector2((column + 1) * width1, row * height1);
                uv[2] = new Vector2(column * width1, (row + 1) * height1);
                uv[3] = new Vector2((column + 1) * width1, (row + 1) * height1); 
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawImage);

            }
        }
    }
private void scatter()
{
    float orthoHeight = Camera.main.orthographicSize;
    float screenAspect = (float)Screen.width / Screen.height;
    float orthoWidth = orthoHeight * screenAspect;
    
    float pieceWidth = width * pieceHolder.localScale.x;
    float pieceHeight = height * pieceHolder.localScale.y;
    
    orthoHeight -= pieceHeight;
    orthoWidth -= pieceWidth;
    float boardX = pieceHolder.position.x;


    List<Rect> placedPieces = new List<Rect>();
    
foreach (Transform piece in jigsawPieces)
{
        bool validPosition = false;
        float z = 0, y = 0;
        int attempts = 0;
        const int maxAttempts = 100;

    while (!validPosition && attempts < maxAttempts)
    {
    z = Random.Range(-orthoWidth, orthoWidth/4);
    y = Random.Range(0,orthoHeight*1.5f);
    Rect newPieceRect = new Rect(
        z - pieceWidth/2, 
        y - pieceWidth/2,
        pieceWidth, 
        pieceHeight
        );
    validPosition = true;
    
    foreach (Rect placedPiece in placedPieces)
    {
        if (newPieceRect.Overlaps(placedPiece))
        {
            validPosition = false;
            break;
        }
    }
    
        if (validPosition)
        {
            placedPieces.Add(newPieceRect);
            break;
        }
        attempts++;
        }
    piece.position = new Vector3(boardX, y, z);
    }
}
    private void updateBorder() 
    {
        LineRenderer lineRenderer = pieceHolder.GetComponent<LineRenderer>();
        float halfWidth = (dimensions.x * width) / 2;
        float halfHeight = (dimensions.y * height) / 2;

        float borderZ = 0f;
        lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = true;
    }

    private void snapCheck()
    {
        int pieceIndex = jigsawPieces.IndexOf(draggedPiece);
        int col = pieceIndex % dimensions.x;
        int row = pieceIndex / dimensions.x;
        Vector2 targetPosition = new Vector2(
            -width * dimensions.x / 2 + (width * col)+(width/2), 
            (-height * dimensions.y / 2) + (height * row)+(height/2));
        if (Vector2.Distance(draggedPiece.localPosition, targetPosition) < (width / 2))
        {
            draggedPiece.localPosition = targetPosition;
            draggedPiece.GetComponent<BoxCollider2D>().enabled = false;
            piecesCorrect++;
            if (piecesCorrect == jigsawPieces.Count)
            {
                Debug.Log("Puzzle Completed!");
                // Do something here
            }
        }

    }
    void Update()
    {
        if (leftBumper.action.WasPressedThisFrame() || rightBumper.action.WasPressedThisFrame())
        {
            RaycastHit hit;
            bool leftHit = rayInteractorLeft.TryGetCurrent3DRaycastHit(out hit);
            bool rightHit = false;
            if (!leftHit)
            {
                rightHit = rayInteractorRight.TryGetCurrent3DRaycastHit(out hit);
            }

            if (leftHit || rightHit)
            {
                draggedPiece = hit.transform;
                Vector3 controllerPosition = leftHit ? 
                    rayInteractorLeft.transform.position : 
                    rayInteractorRight.transform.position;
                offset = draggedPiece.position - Camera.main.ScreenToWorldPoint(controllerPosition);
            }
        }

        // Check if the piece is being dragged and update its position to raycast hit point
        if (draggedPiece)
        {
            Vector3 controllerPosition = leftBumper.action.IsPressed() ? 
                rayInteractorLeft.transform.position : 
                rayInteractorRight.transform.position;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(controllerPosition);
            newPosition.z += offset.z;
            draggedPiece.position = newPosition;
        }

        if (draggedPiece && (leftBumper.action.WasReleasedThisFrame() || rightBumper.action.WasReleasedThisFrame()))
        {
            snapCheck();
            draggedPiece.position += Vector3.forward;
            draggedPiece = null;
        }
    }
}
