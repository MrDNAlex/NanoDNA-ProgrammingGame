using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;
using UnityEditor;

[CreateAssetMenu]
public class CharLedger : ScriptableObject
{

    public List<CharInstance> chars;

    public bool validate;

    private void OnValidate()
    {

        CharInstance[] idk = (CharInstance[])Resources.FindObjectsOfTypeAll(typeof(CharInstance));

        chars = idk.ToList();

    }

}
