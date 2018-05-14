using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*this script is mainly made for the partcicles to slow down after they reach the doorway
 * this can be done using gravity modifier in the edit but it doesnt look really nice
 * so the idea is to speed up the simulation speed a lot, wait a fraction of a second and then 
 * really slow down the partciles to give it the smokey and spooky effect
 */
public class ParticleSample : MonoBehaviour {
    private ParticleSystem _ps;
	// Use this for initialization
	void Start () {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(SimulationSpeedParticles());
	}
	
    IEnumerator SimulationSpeedParticles(){
        var main = _ps.main;
        main.simulationSpeed = 1000f;
        _ps.Play();
        yield return new WaitForSeconds(0.1f);
        main.simulationSpeed = 0.05f;
    }
}
