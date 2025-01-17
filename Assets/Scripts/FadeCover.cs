﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCover : MonoBehaviour
{
 
    public GameObject player;
    public Shader shaderDifuse;
    public Shader shaderTransparent;
    public float targetAlpha;
    public float time;
    public GameObject o;
    public bool mustFadeBack = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter");
        shaderDifuse = Shader.Find("Diffuse");
        shaderTransparent = Shader.Find("Transparent/Diffuse");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, 30))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Environment")) //CanHide
            {
                mustFadeBack = true;

                if (hit.collider.gameObject != o && o != null)
                {
                    FadeUp(o);
                }

                o = hit.collider.gameObject;

                if (o.GetComponent<Renderer>().material.shader != shaderTransparent)
                {
                    o.GetComponent<Renderer>().material.shader = shaderTransparent;
                    Color k = o.GetComponent<Renderer>().material.color;
                    k.a = 0.5f;
                    o.GetComponent<Renderer>().material.color = k;
                }

                FadeDown(o);
            }
            else
            {
                if (mustFadeBack)
                {
                    mustFadeBack = false;
                    FadeUp(o);
                }
            }
        }
    }

    void FadeUp(GameObject f)
    {
        //iTween.Stop(f);
        iTween.FadeTo(f, iTween.Hash("alpha", 1, "time", time, "oncomplete", "SetDifuseShading", "oncompletetarget", this.gameObject, "oncompleteparams", f));
    }

    void FadeDown(GameObject f)
    {
        //iTween.Stop(f);
        iTween.FadeTo(f, iTween.Hash("alpha", targetAlpha, "time", time));
    }

    void SetDifuseShading(GameObject f)
    {
        if (f.GetComponent<Renderer>().material.color.a == 1)
        {
            f.GetComponent<Renderer>().material.shader = shaderDifuse;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, player.transform.position - transform.position);
    }
 }