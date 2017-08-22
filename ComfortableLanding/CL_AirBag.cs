﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KSP.Localization;

public class CL_AirBag : PartModule
{
    [KSPField]
    ModuleAnimateGeneric InflateAnim;

    [KSPField]
    AudioClip inflateSound;
    AudioSource audioSource;
    AudioClip deflateSound;
    AudioSource audioSource2;

    [KSPField]
    public double inflateAltitude = 50.0;
    public float crashToleranceAfterInflated = 45.0f;
    public string DeflateTransformName = "DeflateTransform";
    public float deflateScaleX = 1.0f;
    public float deflateScaleY = 1.0f;
    public float deflateScaleZ = 1.0f;
    public string InflateTransformName = "InflateTransform";
    public float inflateScaleX = 0.1f;
    public float inflateScaleY = 0.1f;
    public float inflateScaleZ = 0.1f;

    public string inflateSoundPath = "ComfortableLanding/Sounds/Inflate_A";
    public string deflateSoundPath = "ComfortableLanding/Sounds/Touchdown";

    public float volume = 1.0f;
    public float volume2 = 1.0f;

    private Transform DeflateTransform = null;
    private Transform InflateTransform = null;

    public override void OnStart(StartState state)
    {
        InflateAnim = part.Modules["ModuleAnimateGeneric"] as ModuleAnimateGeneric;
        if (InflateAnim == null)
            Debug.Log("<color=#FF8C00ff>Comfortable Landing:</color>Animation Missing!");

        DeflateTransform = base.part.FindModelTransform(DeflateTransformName);
        InflateTransform = base.part.FindModelTransform(InflateTransformName);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.bypassListenerEffects = true;
        audioSource.minDistance = .3f;
        audioSource.maxDistance = 1000;
        audioSource.priority = 10;
        audioSource.dopplerLevel = 0;
        audioSource.spatialBlend = 1;
        inflateSound = GameDatabase.Instance.GetAudioClip(inflateSoundPath);
        audioSource.clip = inflateSound;
        audioSource.loop = false;
        audioSource.time = 0;
        if (volume < 0)
        {
            volume = 0.0f;
        }
        else if (volume > 1)
        {
            volume = 1.0f;
        }
        audioSource.volume = volume;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.bypassListenerEffects = true;
        audioSource2.minDistance = .3f;
        audioSource2.maxDistance = 1000;
        audioSource2.priority = 10;
        audioSource2.dopplerLevel = 0;
        audioSource2.spatialBlend = 1;
        deflateSound = GameDatabase.Instance.GetAudioClip(deflateSoundPath);
        audioSource2.clip = deflateSound;
        audioSource2.loop = false;
        audioSource2.time = 0;
        if (volume2 < 0)
        {
            volume2 = 0.0f;
        }
        else if (volume2 > 1)
        {
            volume2 = 1.0f;
        }
        audioSource.volume = volume2;
    }

    public void Inflate()
    {
        ScreenMessages.PostScreenMessage("<color=#00ff00ff>[ComfortableLanding]Inflate!</color>", 3f, ScreenMessageStyle.UPPER_CENTER);
        audioSource.PlayOneShot(inflateSound);
        InflateAnim.allowManualControl = true;
        InflateAnim.Toggle();
        InflateAnim.allowManualControl = false;
        Debug.Log("<color=#FF8C00ff>Comfortable Landing:</color>Inflate!");
    }

    public void Deflate()
    {
        ScreenMessages.PostScreenMessage("<color=#00ff00ff>[ComfortableLanding]Touchdown!</color>", 3f, ScreenMessageStyle.UPPER_CENTER);
        audioSource2.PlayOneShot(deflateSound);
        InflateTransform.localScale = new Vector3(inflateScaleX, inflateScaleY, inflateScaleZ);
        DeflateTransform.localScale = new Vector3(deflateScaleX, deflateScaleY, deflateScaleZ);
        Debug.Log("<color=#FF8C00ff>Comfortable Landing:</color>Touchdown!");
    }
}