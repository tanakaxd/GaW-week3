using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public List<Texture> textures;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture GetRandomTexture()
    {
        Texture texture = textures[Random.Range(0, textures.Count)];
        return texture;
    }
}
