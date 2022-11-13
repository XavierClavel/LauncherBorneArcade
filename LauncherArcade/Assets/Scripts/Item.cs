using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Item
{
    public string name;
    public Sprite image;
    const float PixelsPerUnit = 100.0f;

    ///<summary>
    ///Gets executed when user presses Enter key.
    ///</summary>
    virtual public void OnEnter()
    {

    }

    ///<summary>
    ///Gets executed when user presses Info key.
    ///</summary>
    public virtual void OnInfo()
    {

    }

    public string loadText(string pathToText, string defaultText)
    {
        var strBuilder = new StringBuilder();
        if (File.Exists(pathToText))
        {
            StreamReader reader = new StreamReader(pathToText);

            while (!reader.EndOfStream)
            {
                strBuilder.Append(reader.ReadLine() + "\n");
            }

            reader.Close();
        }
        else
        {
            strBuilder.Append(defaultText);
        }
        return strBuilder.ToString();
    }

    public void loadImage(string pathToImage)
    {
        //Load image no matter name of the file
        //string[] imageFiles = Directory.GetFiles(gameToShow.pathToGameMeta, "*.png*", SearchOption.AllDirectories);
        //if (imageFiles.Length > 0) pathToGameLogo = imageFiles[0];

        if (File.Exists(pathToImage))
        {
            Texture2D spriteTexture = LoadTexture(pathToImage);
            image = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            Resources.UnloadUnusedAssets(); //prevent memory leak
        }
        else
        {
            image = Resources.Load<Sprite>("logo");
        }
    }

    Texture2D LoadTexture(string filePath)
    {
        Texture2D tex2D;
        byte[] fileData;
        fileData = File.ReadAllBytes(filePath);
        tex2D = new Texture2D(2, 2);           // Create new "empty" texture
        if (tex2D.LoadImage(fileData))
        {          // Load the imagedata into the texture (size is set automatically)
            return tex2D;                 // If data = readable -> return texture
        }
        return null;
    }
}
