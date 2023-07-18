using UnityEngine;

public class XRayMat : MonoBehaviour
{
    public Material origin;
    public Material change;

    private Renderer renderer;
    private XRayControl script;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        script = GameObject.Find("EventSystem").GetComponent<XRayControl>();
    }

    public void ChangeMaterial()
    {
        if (script.isXRay) { renderer.material = change; }
        else { renderer.material = origin; }
    }
}