using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ResourceText : MonoBehaviour {

    private TextMesh mesh;

    void Awake () {

        mesh = GetComponent<TextMesh>();
        gameObject.SetActive(false);
        //gameObject.hideFlags = HideFlags.HideInHierarchy;

    }

    public void TweenResourceText (int _value) {

        mesh.text = "" + _value;
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.DOLocalMoveY(5, 1, false).OnComplete(OnTweenCompleted).OnUpdate(OnTweenUpdate);
        
    }

    private void OnTweenCompleted () {

        gameObject.SetActive(false);

    }

    private void OnTweenUpdate () {

        transform.LookAt(Camera.main.transform);

    }
}
