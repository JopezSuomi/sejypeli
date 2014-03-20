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
    PhysicsObject joel5;
    DoubleMeter pelaaja1mana;
    public override void Begin()
    {
        LuoElamaLaskuri();
        ManaPalkki();
        IsMouseVisible = true;
        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.LimeGreen;
        
        
        

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        joel5 = new PhysicsObject(90, 180);
        Add(joel5);
        joel5.Image = LoadImage("joel5");
        joel5.CanRotate = false;
        joel5.Restitution = 0;

        joel4 = new PhysicsObject(50, 70);
        joel4.Position = new Vector(0, -40);
        Add(joel4, 1);
        AxleJoint liitos = new AxleJoint(joel5, joel4, new Vector(-40, 0) );
        joel5.CollisionIgnoreGroup = 1;
        joel4.CollisionIgnoreGroup = 1;
        
        Add(liitos);
        joel4.Image = LoadImage("joel4");
        joel4.AngularVelocity = 100;
        joel4.Mass = 0.001;

        Level.Background.Image = LoadImage("kentta");
        Level.Background.FitToLevel();





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
        joel5.Push(vektori);
    }
    void LuoElamaLaskuri()
    {
        HorizontalLayout asettelu = new HorizontalLayout();
        asettelu.Spacing = 10;

        Widget sydamet = new Widget(asettelu);
        sydamet.Color = Color.Transparent;
        sydamet.X = Screen.Center.X;
        sydamet.Y = Screen.Top - 30;
        Add(sydamet);

        for (int i = 0; i < 10; i++)
        {
            Widget sydan = new Widget(30, 30, Shape.Heart);
            sydan.Color = Color.Red;
            sydamet.Add(sydan);
        }
    }
    void ManaPalkki()
    {
        pelaaja1mana = new DoubleMeter(100);
        pelaaja1mana.MaxValue = 100;
        BarGauge pelaaja1ElamaPalkki = new BarGauge(20, Screen.Width / 3);
        pelaaja1ElamaPalkki.X = Screen.Left + Screen.Width / 2                 ;
        pelaaja1ElamaPalkki.Y = Screen.Top - 70;
        pelaaja1ElamaPalkki.Angle = Angle.FromDegrees(90);
        pelaaja1ElamaPalkki.BindTo(pelaaja1mana);
        pelaaja1ElamaPalkki.Color = Color.Red;
        pelaaja1ElamaPalkki.BarColor = Color.Green;
        Add(pelaaja1ElamaPalkki);
    }
}
