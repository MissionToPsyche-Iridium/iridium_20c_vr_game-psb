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
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private GameObject leftHandPause;
        [SerializeField] private GameObject rightHandPause;        
        private int piecesCorrect = 0;
        private Dictionary<Transform, Vector3> correctPositions = new Dictionary<Transform, Vector3>();
    [SerializeField] private float snapThreshold = 0.05f; // Tweak for snapping sensitivity
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

        updateBorder();
        scatter();
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
        width = aspect / dimensions.x;

        // Adjust offsets to center the pieces within the frame
        float xOffset = -width * (dimensions.x - 1) / 2;
        float yOffset = -height * (dimensions.y - 1) / 2;

        for (int row = 0; row < dimensions.y; row++)
        {
            for (int column = 0; column < dimensions.x; column++)
            {
                Transform piece = Instantiate(piecePrefab, pieceHolder);
                piece.localPosition = new Vector3(
                    xOffset + (width * column), 
                    yOffset + (height * row), 
                    0 // Ensure Z is aligned with the pieceHolder
                );
                // Store the original local position for snapping
                Vector3 correctLocalPos = piece.localPosition;
                correctPositions[piece] = pieceHolder.TransformPoint(correctLocalPos);
                
                piece.localScale = new Vector3(width, height, 1);
                piece.name = $"Piece {row * dimensions.x + column}";
                piece.gameObject.tag = "Piece";
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
        z = Random.Range(-orthoWidth, 0f);
        y = Random.Range(1,orthoHeight*1.5f);
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

        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.enabled = true;
    }

private void snapCheck()
{
    // Get the position of the dragged piece and check if it's close to the correct position
    Vector3 correctPos = correctPositions[draggedPiece];

    // Calculate the distance between the dragged piece and the correct position
    float distance = Vector3.Distance(draggedPiece.position, correctPos);

    // If the distance is less than the snap threshold, snap the piece to the correct position and disable it
    if (distance <= snapThreshold)
    {
        draggedPiece.position = correctPos;

        draggedPiece.GetComponent<Collider>().enabled = false;

        piecesCorrect++;
        
        if (piecesCorrect >= jigsawPieces.Count)
        {
            Debug.Log("Puzzle Completed!");
            // You can trigger any win animation, UI, or effects here
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
            
            if ((leftHit || rightHit) && hit.transform.CompareTag("Piece"))
            {
                draggedPiece = hit.transform;
                Vector3 rayPosition = leftHit ? 
                    rayInteractorLeft.transform.position : 
                    rayInteractorRight.transform.position;
                // Store only Y and Z offset, keep original X
                float originalX = draggedPiece.position.x;
                offset = new Vector3(0,  // Lock X offset
                    draggedPiece.position.y - hit.point.y,
                    draggedPiece.position.z - hit.point.z);
                draggedPiece.position = new Vector3(originalX, 
                    draggedPiece.position.y, 
                    draggedPiece.position.z);
            }
        }

        if (draggedPiece)
        {
            RaycastHit hit;
            bool rayHit = leftBumper.action.IsPressed() ? 
                rayInteractorLeft.TryGetCurrent3DRaycastHit(out hit) : 
                rayInteractorRight.TryGetCurrent3DRaycastHit(out hit);
        
            if (rayHit)
            {
                // Keep original X position while updating Y and Z
                draggedPiece.position = new Vector3(
                    draggedPiece.position.x,  // Keep X position constant
                    hit.point.y + offset.y,
                    hit.point.z + offset.z
                );
            }
        }

        if (draggedPiece && (leftBumper.action.WasReleasedThisFrame() || rightBumper.action.WasReleasedThisFrame()))
        {
            snapCheck();
            
            draggedPiece = null;
        }
    }
}
