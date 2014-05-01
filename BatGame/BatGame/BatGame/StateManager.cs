using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatGame
{
    public enum GameState
    {
        menu,
        game,
        pause,
        level1,
        level2
    }

    public enum PlayerState
    {
        Idle,
        Flying
    }

    

    class StateManager
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
    }//end class
}//end namespace
