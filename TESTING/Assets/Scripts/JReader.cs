using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class JReader : MonoBehaviour
{
    public TextAsset textJSON;

    public void LoadJson()
    {
        Characters charJson = JsonUtility.FromJson<Characters>(textJSON.text);
        foreach (Character character in charJson.characters)
        {
            Debug.Log("Character" + character.name);
        }
    }
}

// NPC
[System.Serializable]
public class Characters
{
    public Character[] characters;
}

[System.Serializable]
public class Character
{
    public string name;
    public Dialog[] dialogs;
}

[System.Serializable]
public class Dialog
{
    public int id;
    public string dialog_message;
    public Choice[] choices;
    public int article;
    public string objectives;
    public string clues;
    public string minigame;
    public string path;
}

[System.Serializable]
public class Choice
{
    public int target_id;
    public string choice_message;
}

// INFORMANT
[System.Serializable]
public class Informant
{
    public string name;
    public InformantDialog[] informant_dialogs;
}

[System.Serializable]
public class InformantDialog
{
    public string dialog_message;
    public string panel;
}

// ARTICLES
[System.Serializable]
public class Articles
{
    public Article[] articles;
}

[System.Serializable]
public class Article
{
    public int id;
    public string title;
    public PublisherDetail[] publisher;
    public string date;
    public string body;
    public string photo;
    public string credibility;
    public int r_score;
    public int ca_score;
    public string article_conclusion;
}

[System.Serializable]
public class PublisherDetail
{
    public string name;
    public string type;
    public string description;
    public string pub_photo;
    public string clue;
}

// MINIGAME ARTICLES
[System.Serializable]
public class MG_Articles
{
    public MG_Article[] mg_articles;
}

[System.Serializable]
public class MG_Article
{
    public int id;
    public string title;
    public MG_Publisher[] mg_publisher;
    public string date;
    public string description;
    public string mg_photo;
    public string credibility;
}

[System.Serializable]
public class MG_Publisher
{
    public string name;
    public string type;
    public string description;
    public string pub_photo;
}

// PERSONAL ARTICLES
[System.Serializable]
public class P_Articles
{
    public P_Article[] p_articles;
}

[System.Serializable]
public class P_Article
{
    public int id;
    public string title;
    public string body;
    public string photo;
}

// OBJECT DIALOGUES
[System.Serializable]
public class ObjectDialog
{
    public string name;
    public string object_dialog;
    public string clues;
}

// CONCLUSION SENTENCES
[System.Serializable]
public class Conclusions
{
    public Relevance_Conclusions[] relevance_conclusion;
}

[System.Serializable]
public class Relevance_Conclusions
{
    public int id;
    public string type;
    public string sentence;
}

// GUIDE
[System.Serializable]
public class Guides
{
    public string guide_title;
    public string guide_details;
    public Guide[] guide;
}

[System.Serializable]
public class Guide
{
    public int id;
    public string name;
    public Content[] content;
}

[System.Serializable]
public class Content
{
    public string title;
    public string details;
    public string photo;
    public Examples[] examples;
}

[System.Serializable]
public class Examples
{
    public string example;
}

// TIPS
[System.Serializable]
public class Tips
{
    public Tip[] tips;
}

[System.Serializable]
public class Tip
{
    public string tip;
}