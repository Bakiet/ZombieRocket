using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]

public class GridSnap : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (!Application.isPlaying)
		{
			if (Selection.Contains (gameObject))
			{
				transform.localPosition = new Vector3((int)transform.localPosition.x, (int)transform.localPosition.y, (int)transform.localPosition.z);
			}
		}
#endif
		
	}
}
