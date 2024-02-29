using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiddenObject.GamePlay
{
    public class InputCameraMovement : MonoBehaviour
    {
        private Vector3 TouchStart;
        private Camera CurrentCamera;

        private void Awake()
        {
            CurrentCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchStart = CurrentCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = TouchStart - CurrentCamera.ScreenToWorldPoint(Input.mousePosition);
                CurrentCamera.transform.position += direction;
            }
        }

        private void Zoom(float offset)
        {
            
        }
    }
}

