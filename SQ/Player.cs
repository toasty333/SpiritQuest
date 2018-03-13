﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{

    public class Player : Sprite
    {
        #region playerStats
        // main stats
        // strength, dexterity, willpower, intelligence, charisma and vitality.
        int STR;
        int DEX;
        int WIL;
        int INT;
        int CHA;
        int VIT;

        // behind the scenes stuff
        int currentExperience;
        int requiredExperience;

        // armour being worn
        short equippedHeavy;
        short equippedMedium;
        short equippedLight;

        // resource pools, heath, stamina and magic
        int maxHP;
        int currentHP;
        int maxSP;
        int currentSP;
        int maxMP;
        int currentMP;
        int staminaRegen;
        int magicRegen;

        // derived stats
        int armourRating;
        int shieldRating;
        int baseCriticalRating;
        int encourage;
        int block;
        #endregion

        #region PlayerVariables
        int MovementSpeed;
        int WidthGapBetweenFrame;
        int HeightGapBetweenFrame;
        public Vector2 PositionOnGrid;
        Vector2 GridPositionOffset;
        int ProperGridPosition;
        bool Moving;
        bool Left = true, Right = true, Up = true, Down = true;


        #endregion

        #region PlayerSpecificFunctions
        //this is the constructor for the player
        public Player(Texture2D spriteTexture, Rectangle spritePOS, Rectangle sourceRect, int amountOfFrames, double timeBetweenFrames, int amountOfRows, int movementSpeed, int widthGapBetweenFrame, int heightGapBetweenFrame)
        {
            Moving = false;
            PositionOnGrid = new Vector2(4,4);
            
            MovementSpeed = movementSpeed;
            SpriteTexture = spriteTexture;
            SpritePOS = spritePOS;
            GridPositionOffset = new Vector2((SpritePOS.Width / 2) - 16, (SpritePOS.Height) - 16);
            SpritePOS.X = (int)PositionOnGrid.X * 32 - (int)GridPositionOffset.X;
            SpritePOS.Y = (int)PositionOnGrid.Y * 32 - (int)GridPositionOffset.Y;
            SourceRectangle = sourceRect;
            AmountOfFramesOnEachRow = amountOfFrames + 1;
            TimeBetweenFrames = timeBetweenFrames;
            AmountOfRows = amountOfRows;
            WidthGapBetweenFrame = widthGapBetweenFrame;
            HeightGapBetweenFrame = heightGapBetweenFrame;

            INT = 50;
            STR = 50;
            DEX = 50;
            WIL = 50;
            CHA = 50;
            VIT = 50;

            maxHP = (3 * VIT + (5 * equippedHeavy));
            currentHP = maxHP;
            maxSP = (8 * STR + (5 * equippedMedium));
            currentSP = maxSP;
            maxMP = (8 * WIL + (5 * equippedLight));
            currentMP = maxMP;

            staminaRegen = (5 + (INT / 3) + (5 * equippedLight));
            staminaRegen = (5 + (INT / 3) + (5 * equippedLight));

            armourRating = 0;
            shieldRating = 0;
            baseCriticalRating = (INT / 2);
            encourage = (1 + (CHA / 100));
            block = ((5 + shieldRating) + (VIT / 2));

        }

        public void setMovementBools(bool up, bool left, bool right, bool down)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
        
        public bool getMovingBool()
        {
            return Moving;
        }

        public int getProperGridPosition()
        {
            return ProperGridPosition;
        }
        public override void Animation(GameTime gameTime)
        {
            base.Animation(gameTime);

            ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedTime >= TimeBetweenFrames){

                CurrentFrame += 1;
                

                if (CurrentFrame >= AmountOfFramesOnEachRow )
                    CurrentFrame = 1;
                SourceRectangle.X = (SourceRectangle.Width * CurrentFrame) + (WidthGapBetweenFrame * CurrentFrame);
                ElapsedTime -= TimeBetweenFrames;
            }
           
        }

        public void setProperGridPosition(int numberOfIntsOnRow)
        {
            ProperGridPosition = (int)PositionOnGrid.X + ((int)PositionOnGrid.Y * numberOfIntsOnRow);
        }
        #endregion

        #region BaseFunctions(UPDATE,DRAW)
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState KB = Keyboard.GetState();
            if (KB.IsKeyDown(Keys.W) && Moving == false)
            {

                SourceRectangle.Y = SourceRectangle.Height;
                if (Up == true)
                {
                    PositionOnGrid.Y -= 1;
                    Moving = true;
                }
                
            }
            if (KB.IsKeyDown(Keys.S) && Moving == false)
            {

                SourceRectangle.Y = 0;
                if (Down == true)
                {
                    PositionOnGrid.Y += 1;
                    Moving = true;
                }
                
            }
            if (KB.IsKeyDown(Keys.A) && Moving == false)
            {
                SourceRectangle.Y = SourceRectangle.Height * 2;
                if (Left == true)
                {
                    Moving = true;
                    PositionOnGrid.X -= 1;
                }

            }
            if(KB.IsKeyDown(Keys.D) && Moving == false)
            {
                SourceRectangle.Y = SourceRectangle.Height * 3;
                if (Right == true)
                {
                    Moving = true;
                    PositionOnGrid.X += 1;
                }
            }

            if (Moving)
            {
                Animation(gameTime);
                
                if (SpritePOS.X > PositionOnGrid.X * 32 - GridPositionOffset.X )
                {
                    SpritePOS.X -= 4;
                }
                else if (SpritePOS.X < PositionOnGrid.X * 32 - GridPositionOffset.X)
                {
                    SpritePOS.X += 4;
                }
                else if (SpritePOS.Y < PositionOnGrid.Y * 32 - GridPositionOffset.Y)
                {
                    SpritePOS.Y += 4;
                }
                else if(SpritePOS.Y > PositionOnGrid.Y * 32 - GridPositionOffset.Y)
                {
                    SpritePOS.Y -= 4;
                }
                else
                {
                    Moving = false;
                }
                
            }
            else
            {
                SourceRectangle.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(SpriteTexture, SpritePOS, SourceRectangle, Color.White);
        }
        #endregion
    }
}
