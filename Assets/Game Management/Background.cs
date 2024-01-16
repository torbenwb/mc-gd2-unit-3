using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public static Background instance;
    public Image backgroundImage;
    public List<Sprite> backgroundSprites;
    public int spriteIndex;
    void Awake()
    {
        if (instance) Destroy(this);
        else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void ToggleSpriteIndex()
    {
        spriteIndex++;
        if (spriteIndex >= backgroundSprites.Count) spriteIndex = 0;
        backgroundImage.sprite = backgroundSprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) ToggleSpriteIndex();
    }
}
