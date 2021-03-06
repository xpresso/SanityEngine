using UnityEngine;
using System.Collections;

[AddComponentMenu("Sanity Engine/User Interaction/Ray Mouse Interactor")]
public class RayMouseInteractor : MonoBehaviour {
	public enum MouseButton {
		LEFT = 0,
		RIGHT = 1,
		MIDDLE = 2
	}
	
	public LayerMask clickLayerMask = -1;
	public MouseButton moveButton = MouseButton.RIGHT;
	
	void OnGUI()
	{
		if(Event.current.type != EventType.MouseDown) {
			return;
		}
		
		if(Event.current.button != (int)moveButton) {
			return;
		}
		
		Vector3 pos = Event.current.mousePosition;
		pos.y = Screen.height - pos.y;
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity,
			clickLayerMask.value);
		foreach(RaycastHit hit in hits)	{
			GameObjectNode node = hit.transform.GetComponent<GameObjectNode>();
			if(node != null) {
				SendMessage("SetGoalNode", node);
				break;
			}
		}
		
		Event.current.Use();
	}	
}
