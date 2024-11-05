using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to scroll the background image. attach to each individual image used for parallaxing
/// </summary>
public class ParallaxShift : MonoBehaviour
{
    [SerializeField] float moveSpeed; 
    //[SerializeField] float alphavalue = 255f;
    Material material;
    Renderer renderMe;
    Vector2 offset;        
    float offsety;
    Color color;

    private void Awake()
    {
      renderMe = GetComponent<Renderer>();
      material = renderMe.material;        
      offset = new Vector2(material.mainTextureOffset.x, material.mainTextureOffset.y);
      //previousOffset = material.mainTextureOffset;

    }

    void Update()
    {
        // need to figure out how to keep the offset nominal.
        float movement = Mathf.Repeat(moveSpeed * Time.deltaTime , 1f);        

        offsety = Mathf.Repeat(movement , 1f);        
        
        material.mainTextureOffset = offset + new Vector2(0f, offsety); 
        
        offset = material.mainTextureOffset;
        
        //previousOffset = material.mainTextureOffset;

        






    }
}
