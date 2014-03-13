using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class UrheaSotilas : PhysicsGame
{
    PhysicsObject joel4; 
    PhysicsObject joel3;
    public override void Begin()
    {

        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.LimeGreen;
        
        
        

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        joel3 = new PhysicsObject(90, 180);
        Add(joel3);
        joel3.Image = LoadImage("joel3");

        joel4 = new PhysicsObject(20, 30);
        Add(joel4);
        joel4.Image = LoadImage("joel4");

        Keyboard.Listen(Key.Left, ButtonState.Down,
    LiikutaPelaajaa, null, new Vector(-450, 0));
        Keyboard.Listen(Key.Right, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(450, 0));
        Keyboard.Listen(Key.Up, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(0, 450));
        Keyboard.Listen(Key.Down, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(0, -450));
    }
    void LiikutaPelaajaa(Vector vektori)
    {
        joel3.Push(vektori);
    }
}
