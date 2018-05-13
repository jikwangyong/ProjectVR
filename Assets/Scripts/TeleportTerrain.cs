﻿
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    public class TeleportTerrain : TeleportMarkerBase
    {
        //Public properties
        public Bounds meshBounds { get; private set; }

        //Private data
        private TerrainCollider areaMesh;
        private int tintColorId = 0;
        private Color visibleTintColor = Color.clear;
        private Color highlightedTintColor = Color.clear;
        private Color lockedTintColor = Color.clear;
        private bool highlighted = false;

        //-------------------------------------------------
        public void Awake()
        {
            areaMesh = GetComponent<TerrainCollider>();

            tintColorId = Shader.PropertyToID("_TintColor");

            CalculateBounds();
        }


        //-------------------------------------------------
        public void Start()
        {
            visibleTintColor = Teleport.instance.areaVisibleMaterial.GetColor(tintColorId);
            highlightedTintColor = Teleport.instance.areaHighlightedMaterial.GetColor(tintColorId);
            lockedTintColor = Teleport.instance.areaLockedMaterial.GetColor(tintColorId);
        }


        //-------------------------------------------------
        public override bool ShouldActivate(Vector3 playerPosition)
        {
            return true;
        }


        //-------------------------------------------------
        public override bool ShouldMovePlayer()
        {
            return true;
        }


        //-------------------------------------------------
        public override void Highlight(bool highlight)
        {
            if (!locked)
            {
                highlighted = highlight;
                
            }
        }


        //-------------------------------------------------
        public override void SetAlpha(float tintAlpha, float alphaPercent)
        {
            Color tintedColor = GetTintColor();
            tintedColor.a *= alphaPercent;
        }


        //-------------------------------------------------
        public override void UpdateVisuals()
        {
        }


        //-------------------------------------------------
        public void UpdateVisualsInEditor()
        {
        }


        //-------------------------------------------------
        private bool CalculateBounds()
        {
            TerrainCollider terrainCollider = GetComponent<TerrainCollider>();
            if (terrainCollider == null)
            {
                return false;
            }
            
            meshBounds = terrainCollider.bounds;
            return true;
        }


        //-------------------------------------------------
        private Color GetTintColor()
        {
            if (locked)
            {
                return lockedTintColor;
            }
            else
            {
                if (highlighted)
                {
                    return highlightedTintColor;
                }
                else
                {
                    return visibleTintColor;
                }
            }
        }
    }
    
}
