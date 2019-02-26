using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour {
	[SerializeField] private string itemName = null;

	void OnTriggerEnter(Collider other) {
		Managers.Inventory.AddItem(itemName);
		Destroy(this.gameObject);
	}
}
