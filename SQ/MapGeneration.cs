﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LitJson;

namespace SQ
{
    class MapGeneration
    {

        List<MapLayer> MapLayers;
        JsonData jsonData;

        public void CreateMap(ContentManager content)
        {
            MapLayers = new List<MapLayer>();
            if (!File.Exists("MapData/Ground.json"))
            {
                Directory.CreateDirectory("MapData");
              
                string json = @"
                    {
                        ""NumberOfFloorObjects"" : 10,
                        
                        ""MapData"" : [
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1],     

                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1] ,

                         [0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0] ,
                                         
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
                        
                         [0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 4, 0, 0, 3, 1, 2, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0] 
                            
                          ]
                    }
                ";

                

                jsonData = JsonMapper.ToObject(json);
                File.WriteAllText("MapData/Ground.json", jsonData.ToJson());
            }
            for (int i = 0; i <= 4; i++)
            {
                MapLayers.Add(new MapLayer());
                MapLayers[i].CreateMap(content, i);
            }
            

        }

        public void Update(GameTime gameTime, Camera cam)
        {

        }

        public bool  CanPlayerMoveToTile(int tileNumber)
        {
            if (tileNumber < MapLayers[i].Length && tileNumber > 0)
            {
                if (Collision[tileNumber] == 1)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }


        public void DrawBeforePlayer(int playerPos, SpriteBatch spriteBatch)
        {

        }

        public void DrawAfterPlayer(int playerPos, SpriteBatch spriteBatch)
        {
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
    public class TexturePosition
    {

        Rectangle Position;
        Rectangle Source;

        public TexturePosition(Rectangle Pos, Rectangle source)
        {
            Position = Pos;
            Source = source;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture) 
        {
            spriteBatch.Draw(texture,Position,Source,Color.White);
        }
    }

    public class MapLayer
    {
        Texture2D MapTextures;
        TexturePosition[] MapTexturePositions;
        public int[] MapNumberArray { get;1 private set; }
        int NumberOfFloorObjects;
        int MapDataNumber;
        public MapLayer() { }

        public void CreateMap(ContentManager Content, int MapDataNumber ) 
        {
            JsonData jsonData = JsonMapper.ToObject(File.ReadAllText("MapData/Ground.json").ToString());
            JsonReader reader = new JsonReader(jsonData["MapData"][MapDataNumber].ToJson().ToString());

            this.MapDataNumber = MapDataNumber;

            MapNumberArray = JsonMapper.ToObject<int[]>(reader);
            NumberOfFloorObjects = (int)jsonData["NumberOfFloorObjects"];

            if (NumberOfFloorObjects * NumberOfFloorObjects != MapNumberArray.Length)
            {
                // if this runs then the mapdata is smaller or lager than the number of floor objects that should be in it
                Array.Clear(MapNumberArray, 0, MapNumberArray.Length);
                MapNumberArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                for (int i = 0; i < NumberOfFloorObjects * NumberOfFloorObjects; i++)
                {
                    if(MapDataNumber == 0 || MapDataNumber == 3)
                    {
                        MapNumberArray[i] = 0;
                    }
                    else
                    {
                        MapNumberArray[i] = -1;
                    }
                }

                // Textures being made based on map data
                if (MapDataNumber == 0)
                {
                    MapTextures = Content.Load<Texture2D>("BaseLayer");
                }
                else if(MapDataNumber == 1)
                {
                    MapTextures = Content.Load<Texture2D>("Layer1");
                }
                else if (MapDataNumber == 2)
                {
                    MapTextures = Content.Load<Texture2D>("Layer2");
                }
                else if(MapDataNumber == 3)
                {
                    MapTextures = Content.Load<Texture2D>("Collisions");
                }
                else
                {
                    MapTextures = Content.Load<Texture2D>("Items");
                }

                for (int i = 0; i < NumberOfFloorObjects; i++)
                {
                    for (int I = 0; I < NumberOfFloorObjects; I++)
                    {
                        //change the the number that xy are divided by (in this case 8) to change the number of
                        //items on a row in the sprite sheet
                        if (MapNumberArray[(I + (i * NumberOfFloorObjects))] != -1)
                        {
                            int X, Y;
                            X = (MapNumberArray[(I + (i * NumberOfFloorObjects))] % 8) * 32;
                            Y = (MapNumberArray[(I + (i * NumberOfFloorObjects))] / 8) * 32;
                            MapTexturePositions[(i * NumberOfFloorObjects) + I] = new TexturePosition(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));
                        }
                    }
                }

            }


        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            // if collisions or BaseLayer
            if(MapDataNumber == 0 || MapDataNumber == 3)
            {
                foreach (TexturePosition t in MapTexturePositions)
                {
                    t.Draw(spriteBatch, MapTextures);
                }
            }
        }

        public void DrawAfterPlayer(int playerPos, SpriteBatch spriteBatch)
        {
            if (MapDataNumber == 1)
            {
                for (int i = playerPos - 2; i < MapTexturePositions.Length; i++)
                {
                    if (MapTexturePositions[i] != null && i >= 0)
                        MapTexturePositions[i].Draw(spriteBatch, MapTextures);
                }
            }
            else if (MapDataNumber == 2)
            {
                for (int i = 0; i < MapTexturePositions.Length; i++)
                {
                    if (MapTexturePositions[i] != null && i >= 0)
                        MapTexturePositions[i].Draw(spriteBatch, MapTextures);
                }
            }
        }

        public void DrawBeforePlayer(int playerPos, SpriteBatch spriteBatch)
        {
            if (MapDataNumber == 1) {
                for (int i = 0; i <= playerPos - 2; i++)
                {
                    if (MapTexturePositions[i] != null)
                        MapTexturePositions[i].Draw(spriteBatch, MapTextures);
                }
            }
            else if (MapDataNumber == 4)
            {
                for (int i = 0; i < MapTexturePositions.Length; i++)
                {
                    if (MapTexturePositions[i] != null && i >= 0)
                        MapTexturePositions[i].Draw(spriteBatch,MapTextures );
                }
            }
        }
        
    }
    
}
