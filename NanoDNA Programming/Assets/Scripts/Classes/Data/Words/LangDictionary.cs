using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNAStruct;

public static class LangDictionary
{
    //Variable
    public static UIWord customTab = new UIWord("New", "Nouveau");
    public static UIWord premadeTab = new UIWord("Established", "Établi");
    public static UIWord variable = new UIWord("Variables", "Variables");
    public static UIWord newVariable = new UIWord("New Variable", "Nouvelle Variable");
    public static UIWord setbtn = new UIWord("Set", "Définer");
    public static UIWord error = new UIWord("Error : The variable inputted is not of the type ", "Erreur : La variable n'est pas du type ");

    //Var Types
    public static UIWord Text = new UIWord("Text", "Texte");
    public static UIWord Number = new UIWord("Number", "Nombre");
    public static UIWord Decimal = new UIWord("Decimal", "Decimale");
    public static UIWord Bool = new UIWord("Boolean", "Booléen");

    public static UIWord Public = new UIWord("Public", "Publique");
    public static UIWord Private = new UIWord("Private", "Privée");

    public static UIWord True = new UIWord("True", "Vrai");
    public static UIWord False = new UIWord("False", "Faux");

    public static UIWord Up = new UIWord("Up", "Haut");
    public static UIWord Left = new UIWord("Left", "Gauche");
    public static UIWord Right = new UIWord("Right", "Droite");
    public static UIWord Down = new UIWord("Down", "Bas");

    public static UIWord Whisper = new UIWord("Whisper", "Chuchote");
    public static UIWord Talk = new UIWord("Talk", "Dit");
    public static UIWord Yell = new UIWord("Yell", "Crie");

    public static UIWord EntText = new UIWord("Enter Text...", "Entrez du Texte...");

    public static UIWord Addition = new UIWord("Addition", "Addition");
    public static UIWord Subtraction = new UIWord("Subtraction", "Soustraction");
    public static UIWord Multiplication = new UIWord("Multiplication", "Multiplication");
    public static UIWord Division = new UIWord("Division", "Division");

}
