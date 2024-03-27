using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace HiddenObject.GamePlay
{
    public class CameraViewController : MonoBehaviour
    {
        public List<SpriteRenderer> BGSpriteLst = new List<SpriteRenderer>();

        [Header("Zoom Properties")] public bool ZoomEnable;
        public float ZoomMin = 2f;
        public float ZoomMax = 0.5f;

        private float ZoomPan = 0f;

        [Header("Pan Properties")] public bool PanEnable;

        private float PanMinX, PanMinY, PanMaxX, PanMaxY;
        private Camera MainCam;

        private Vector3 DragOrigin;
        private Vector3 TouchStart;

        private float MinBoundX, MinBoundY, MaxBoundX, MaxBoundY;

        private void Start()
        {
            CalulateBoundaries();
        }

        private void Update()
        {
            if (PanEnable) HandleCameraPan();
            if (ZoomEnable) HandleCameraZoom();
        }

        private void HandleCameraPan()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DragOrigin = MainCam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                var dragDifference = DragOrigin - MainCam.ScreenToWorldPoint(Input.mousePosition);
                MainCam.transform.position = ClampCamera(MainCam.transform.position + dragDifference);
            }
        }

        private void HandleCameraZoom()
        {
        }

        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            var orthographicSize = MainCam.orthographicSize;
            var camWidth = orthographicSize * MainCam.aspect;

            if (MainCam.orthographicSize < ZoomMax + ZoomPan)
            {
                // var position = backgroundSprite.transform.position;
                // var bounds = backgroundSprite.bounds;
                // PanMinX = position.x - bounds.size.x / 2f;
                // PanMinY = position.y - bounds.size.y / 2f;
                // PanMaxX = position.x + bounds.size.x / 2f;
                // PanMaxY = position.y + bounds.size.y / 2f;

                var minX = PanMinX + camWidth;
                var minY = PanMinY + orthographicSize;
                var maxX = PanMaxX - camWidth;
                var maxY = PanMaxY - orthographicSize;

                var clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
                var clampY = Mathf.Clamp(targetPosition.y, minY, maxY);
                return new Vector3(clampX, clampY, targetPosition.z);
            }
            else
            {
                PanMinX = 0f;
                PanMinY = -0.5f;
                PanMaxX = 0;
                PanMaxY = -0.5f;

                var minX = PanMinX; // + camWidth;
                var minY = PanMinY; //+ orthographicSize;
                var maxX = PanMaxX; // - camWidth;
                var maxY = PanMaxY; //- orthographicSize;

                var clampX = Mathf.Clamp(targetPosition.x, minX, maxX);
                var clampY = Mathf.Clamp(targetPosition.y, minY, maxY);
                return new Vector3(clampX, clampY, targetPosition.z);
            }
        }

        private void CalulateBoundaries()
        {
            if (BGSpriteLst != null && BGSpriteLst.Count <= 0) return;
            
            MinBoundX = float.MaxValue;
            MinBoundY = float.MaxValue;
            MaxBoundX = float.MinValue;
            MaxBoundY = float.MinValue;

            foreach (var bg in BGSpriteLst)
            {
                MinBoundX = Mathf.Min(MinBoundX, bg.bounds.min.x);
                MaxBoundX = Math.Max(MaxBoundX, bg.bounds.max.x);
                MinBoundY = Math.Min(MinBoundY, bg.bounds.min.y);
                MaxBoundY = Math.Max(MaxBoundY, bg.bounds.max.y);
                
            }
        }
    }
}