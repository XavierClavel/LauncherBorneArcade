using UnityEngine;
using System.IO;
using System.Text;

public class Game
{
    public string pathToGameDir { get; set; }
    public string pathToExe { get; set; }
    public string pathToGameMeta { get; set; }
    public Sprite logo = null;
    public string name;
    public string description = null;
    const float PixelsPerUnit = 100.0f;

    public Game(string pathToGameDir, string pathToExe, string pathToGameMeta, string name)
    {
        this.pathToGameDir = pathToGameDir;
        this.pathToExe = pathToExe;
        this.pathToGameMeta = pathToGameMeta;
        this.name = name;

        loadLogo();
        loadDescription();
    }

    void loadDescription()
    {
        string pathToDescription = pathToGameMeta + "/description.txt";

        var strBuilder = new StringBuilder();
        if (File.Exists(pathToDescription))
        {
            StreamReader reader = new StreamReader(pathToDescription);

            while (!reader.EndOfStream)
            {
                strBuilder.Append(reader.ReadLine() + "\n");
            }

            reader.Close();
        }
        else
        {
            strBuilder.Append("Un jeu de 7Fault");
        }

        description = strBuilder.ToString();
    }

    void loadLogo()
    {
        //Load image no matter name of the file
        //string[] imageFiles = Directory.GetFiles(gameToShow.pathToGameMeta, "*.png*", SearchOption.AllDirectories);
        //if (imageFiles.Length > 0) pathToGameLogo = imageFiles[0];

        string pathToGameLogo = pathToGameMeta + "/logo.png";
        if (File.Exists(pathToGameLogo))
        {
            Texture2D spriteTexture = LoadTexture(pathToGameLogo);
            logo = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            Resources.UnloadUnusedAssets(); //prevent memory leak
        }
        else
        {
            logo = Resources.Load<Sprite>("logo");
        }
    }

    private Texture2D LoadTexture(string filePath)
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