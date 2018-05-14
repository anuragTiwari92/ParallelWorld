using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS{
    public class PortalController : MonoBehaviour
    {
        public Material[] _myMat;
        public MeshRenderer _portalMesh;
        public UnityARVideo UnityARVideo;

        private bool _inPortal = false;
        private bool _notInPortal = true;

        // Use this for initialization
        void Start()
        {
            OutsidePortal();

        }
		private void OnTriggerStay(Collider other)
		{
            Vector3 _myPos = Camera.main.transform.position +
                                   Camera.main.transform.forward * (Camera.main.nearClipPlane * 4);
            if(transform.InverseTransformPoint(_myPos).z <= 0){
                if(_notInPortal){
                    _notInPortal = false;
                    _inPortal = true;
                    InsidePortal();
                } 
            }
            else{
                if (_inPortal)
                {
                    _notInPortal = true;
                    _inPortal = false;
                    OutsidePortal();
                }
            }
		}
		void OutsidePortal(){
            StartCoroutine(DelayRenderMat(3));
        }
        void InsidePortal()
        {
            StartCoroutine(DelayRenderMat(6));
        }
        // Update is called once per frame
        IEnumerator DelayRenderMat(int stencilNum)
        {
            UnityARVideo._shouldRender = false;
            yield return new WaitForEndOfFrame();
            _portalMesh.enabled = false;
            foreach(Material mat in _myMat){
                mat.SetInt("_Stencil", stencilNum);
            }
            yield return new WaitForEndOfFrame();
            _portalMesh.enabled = true;
            UnityARVideo._shouldRender = true;

        }
    } 
}

