using UnityEngine;
using System.Collections;


/// <summary>
/// This class moves the game-backgroud 
/// </summary>
public class ScrollingBackgroud : MonoBehaviour {


    // A value how fast the background shall move
    [SerializeField]
    private float scrollSpeed;

    private Material mat;

	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        mat.GetTexture("_MainTex").wrapMode = TextureWrapMode.Repeat;
	}
	

	void Update () {

        // Moving the backgound image by setting the texture-offset
        float offset = Mathf.Repeat(Time.time * scrollSpeed, 1);
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
