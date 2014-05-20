//Madison, Karen
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

namespace BatGame
{
    public enum GameState
    {
        menu,
        game,
        pause,
        level1,
        level2,
        partyMode,
        warning
    }

    public enum PlayerState
    {
        Idle,
        Flying
    }

    

    class StateManager:Game1
    {
        PlayerState playerState;

        public StateManager(PlayerState playerS)
        {
            playerState = playerS;
        }

        public PlayerState GetPlayerState
        {
            get
            {
                return playerState;
            }
            set
            {
                playerState = value;
            }
        }

        

        public void GameDraw(GameTime gameTime, GameState gameState, SpriteBatch sp)
        {
            switch (gameState)
            {
                case GameState.menu:
                    this.IsMouseVisible = true;
                    //add menus and buttons into lists or arrays...or dictionaries

                    break;

                case GameState.pause:
                    this.IsMouseVisible = true;
                    //should also used the structure used for menu to take the necessary objects
                    break;

                case GameState.game:
                    //This is a whole different story...don't want to mess with it without Greg because of the shaders....
                    //next time
                    break;
            }
        }
    }//end class
}//end namespace
