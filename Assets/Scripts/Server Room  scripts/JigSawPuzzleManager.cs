using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JigSawPuzzleManager : MonoBehaviour
{
        [Header("Jigsaw Puzzle")]
        [SerializeField] private List<Texture2D> jigsawPuzzleImages;
        [SerializeField] private Transform jigsawBoard;
        [SerializeField] private Image levelSelectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Texture2D texture in jigsawPuzzleImages)
        {
           Image image = Instantiate(levelSelectPrefab, jigsawBoard);
           image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

    }
}
