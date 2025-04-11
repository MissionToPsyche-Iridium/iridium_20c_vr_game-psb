using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JigSawPuzzleManager : MonoBehaviour
{       

        [Header("Game Manager")]
        [Range(3,6)]
        [SerializeField] private int difficulty = 4;

        [Header("Jigsaw Puzzle")]
        [SerializeField] private List<Texture2D> jigsawPuzzleImages;
        [SerializeField] private Transform jigsawBoard;
        [SerializeField] private Image levelSelectPrefab;
        private List<Transform> jigsawPieces;
        private Vector2Int dimensions;
        private float height;
        private float width;
        private float aspect = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Texture2D texture in jigsawPuzzleImages)
        {
           Image image = Instantiate(levelSelectPrefab, jigsawBoard);
           image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

    }

    void createJigsawPieces(Texture2D jigsawImage)
    {
        height = 1f / dimensions.y;
        float aspectRatio = (float)jigsawImage.width / jigsawImage.height;
        width  = aspect / dimensions.x;
    }
}