using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    void Start()
    {
        CombineMeshes();
    }

    void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, false);

        // Birle�tirilmi� mesh'i bir nesneye ekleyin
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;

        // Gereksiz MeshRenderer bile�enlerini devre d��� b�rak�n
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }
        combine = null;
    }

}
