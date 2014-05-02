//Wes and Joel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace BatGame
{
    class PartyMode
    {
        enum DubStep
        {
            lightGreen,
            cyan,
            magenta,
            crimson,
            white,
            yellow
        }

        DubStep stepItUp;
        GraphicsDevice gameOn;
        ImmobilesManager partyPooper;
        GameObjectManager DJ;
        EnemyManager Cops;

        RenderTarget2D lightsTarget;
        RenderTarget2D mainTarget;

        Effect lightingEffect;

        int dontStopTheParty;
        Color LSD;
        Color acid;
        Song tunes;
        bool kickIt;
        SoundEffectInstance playList;

        public PartyMode(GraphicsDevice gameOn, ImmobilesManager partyPooper, GameObjectManager DJ, EnemyManager Cops, Song tune)
        {
            stepItUp = DubStep.white;
            this.gameOn = gameOn;
            this.partyPooper = partyPooper;
            this.DJ = DJ;
            this.Cops = Cops;

            var pp = gameOn.PresentationParameters;
            lightsTarget = new RenderTarget2D(
                gameOn, pp.BackBufferWidth, pp.BackBufferHeight);

            dontStopTheParty = 0;
            LSD = Color.White;
            acid = Color.LightGreen;
            tunes = tune;

        }

        public Color strobeLight(SpriteBatch spriteBatch, Color shrooms)
        {
            if (!kickIt)
            {
                MediaPlayer.Play(tunes);
                kickIt = true;
            }
            //gameOn.SetRenderTarget(lightsTarget);
            if (dontStopTheParty % 5 == 0)
            {
                switch (stepItUp)
                {
                    case DubStep.white:
                        shrooms = Color.White;
                        stepItUp = DubStep.magenta;
                        break;
                    case DubStep.magenta:
                        shrooms = Color.Magenta;
                        stepItUp = DubStep.cyan;
                        break;
                    case DubStep.cyan:
                        shrooms = Color.Cyan;
                        stepItUp = DubStep.lightGreen;
                        break;
                    case DubStep.lightGreen:
                        shrooms = Color.LightGreen;
                        stepItUp = DubStep.crimson;
                        break;
                    case DubStep.crimson:
                        shrooms = Color.Crimson;
                        stepItUp = DubStep.yellow;
                        break;
                    case DubStep.yellow:
                        shrooms = Color.Yellow;
                        stepItUp = DubStep.white;
                        break;
                }
            }
            gameOn.Clear(shrooms);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            /*Vector2 light = new Vector2(player.RectX - 45, player.RectY - 50);
            spriteBatch.Draw(lightMask, light, Color.White);*/
            spriteBatch.End();
            dontStopTheParty++;
            return shrooms;
        }

        public void partyTime(Song jukeBox)
        {
            tunes = jukeBox;
            /*playList = tunes.CreateInstance();
            playList.IsLooped = true;*/
            kickIt = false;
        }

        public void BeerGoggles(SpriteBatch spriteBatch, Player player)
        {
            strobeLight(spriteBatch, LSD);
            acid = strobeLight(spriteBatch, acid);
            spriteBatch.Begin();
            partyPooper.IManagerDrawBack(spriteBatch);
            partyPooper.IManagerDrawFront(spriteBatch);
            //gameObjectManager.GManagerDraw(spriteBatch);
            spriteBatch.Draw(player.ObjTexture, player.ObjRectangle, acid);
            Cops.EManagerDraw(spriteBatch);
            spriteBatch.End();
        }
    }
}
