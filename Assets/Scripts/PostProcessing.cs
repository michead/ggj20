using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChromaticAberration()
    {
        StartCoroutine(_ChromaticAberration());
    }

    private IEnumerator _ChromaticAberration() {
        var done = false;
        var isDescending = false;

        while (!done) {
            var chromaticAberration = GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>();
            chromaticAberration.intensity.value = chromaticAberration.intensity + Time.deltaTime * 2 * (isDescending ? -1F : 1F);

            if (chromaticAberration.intensity >= 1f) {
                isDescending = true;
            }

            if (chromaticAberration.intensity <= 0 && isDescending) {
                break;
            }

            yield return null;
        }
    }
}
