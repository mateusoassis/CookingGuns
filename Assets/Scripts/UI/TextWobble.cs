using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWobble : MonoBehaviour
{
    private TMP_Text textMesh;

    [Header("Horizontal")]
    public float wobbleSin;
    public float horizontalMultiplier;

    [Header("Vertical")]
    public float wobbleCos;
    public float verticalMultiplier;


    private Mesh mesh;
    private Vector3[] vertices;

    private List<int> wordIndexes;
    private List<int> wordLengths;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();

        /*
        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
        */

        /*
        string s = textMesh.text;
        for(int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLenghts.Add(index - wordIndexes[wordIndexes.Count -1]);
            wordIndexes.Add(index + 1);
        }
        wordLenghts.Add(s.Length - wordIndexes[wordIndexes.Count -1]);
        */
    }

    void Update()
    {
        ResetList();
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
 
        //Color[] colors = mesh.colors;
 
        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.unscaledTime + w);
 
            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex+i];
 
                int index = c.vertexIndex;
 
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }
        mesh.vertices = vertices;
        //mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    // Update is called once per frame
    /*
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        
        // word wobble
        for(int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Vector3 offset = Wobble(Time.unscaledTime + w);

            for(int i = 0; i < wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex+i];

                int index = c.vertexIndex;

                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }

        // random wobble
        
        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.unscaledTime + i);

            vertices[i] = vertices[i] + offset;
        }
        
        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    */

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * wobbleSin) * horizontalMultiplier, Mathf.Cos(time * wobbleCos) * verticalMultiplier);
    }

    public void ResetList()
    {
        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }
}
