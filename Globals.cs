using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class Globals
{
    public enum GameState
    {
        Playing,
        GameOver
    }
    public static GameState gameState {get; set;}
    
    public static Random random = new Random();
    public const int CELL_SIZE = 32;
}