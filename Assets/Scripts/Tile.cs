using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] Color hoverColor;
    [SerializeField] Vector3 offset;

    private GameObject turret;

    private Color originalColor;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseDown() {
        if(turret != null) {
            Debug.Log("Space Occupied");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + offset, transform.rotation);
    }

	void OnMouseEnter() {
        rend.material.color = hoverColor;
    }

    void OnMouseExit() {
        rend.material.color = originalColor;
    }
}
