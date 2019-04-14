using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Shooting")]
    [SerializeField] float range = 15f;
    [SerializeField] float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Turret")]
    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 10f;

    [Header("Target")]
    [SerializeField] string enemyTag = "Enemy";
    private Transform target;

    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceEnemy < shortestDistance) {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        }
        else {
            target = null;
        }
    }
	
	void Update () {
		if (target == null) return;

        // Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f) {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
	}

    private void Shoot() {
        GameObject projectileGO = (GameObject) Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null) {
            projectile.Seek(target);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
