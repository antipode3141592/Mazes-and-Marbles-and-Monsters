using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowCaster2dTile : ShadowCaster2D
{
//    private void Awake()
//    {
//        if (m_ApplyToSortingLayers == null)
//            m_ApplyToSortingLayers = SetDefaultSortingLayers();

//        Bounds bounds = new Bounds(transform.position, Vector3.one);

//        Renderer renderer = GetComponent<Renderer>();
//        if (renderer != null)
//        {
//            bounds = renderer.bounds;
//        }
//#if USING_PHYSICS2D_MODULE
//        else
//        {
//            Collider2D collider = GetComponent<Collider2D>();
//            if (collider != null)
//                bounds = collider.bounds;
//        }
//#endif

//        Vector3 relOffset = bounds.center - transform.position;

//        if (m_ShapePath == null || m_ShapePath.Length == 0)
//        {
//            m_ShapePath = new Vector3[]
//            {
//                relOffset + new Vector3(-bounds.extents.x, -bounds.extents.y),
//                relOffset + new Vector3(bounds.extents.x, -bounds.extents.y),
//                relOffset + new Vector3(bounds.extents.x, bounds.extents.y),
//                relOffset + new Vector3(-bounds.extents.x, bounds.extents.y)
//            };
//        }
//    }
}
