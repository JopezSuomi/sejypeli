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
    PhysicsObject Hirvio;
    public override void Begin()
    {
        LuoElamaLaskuri();
        IsMouseVisible = true;
        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.LimeGreen;
        MultiSelectWindow alkuValikko = new MultiSelectWindow("Pelin alkuvalikko",
        "Aloita peli", "Valitse Kenttä", "Lopeta");
        Add(alkuValikko);
        alkuValikko.AddItemHandler(0, AloitaPeli);
        alkuValikko.AddItemHandler(1, ValitseKentta);
        alkuValikko.AddItemHandler(2, Exit);
        
        
        Keyboard.Listen(Key.Left, ButtonState.Down,
    LiikutaPelaajaa, null, new Vector(-450, 0));
        Keyboard.Listen(Key.Right, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(450, 0));
        Keyboard.Listen(Key.Up, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(0, 450));
        Keyboard.Listen(Key.Down, ButtonState.Down,
          LiikutaPelaajaa, null, new Vector(0, -450));


        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

    }

    void LataaHahmot()
    {
        joel5 = new PhysicsObject(90, 180);
        joel5.Tag = "joel5";
        Add(joel5);
        joel5.Image = LoadImage("joel5");
        joel5.CanRotate = false;
        joel5.Restitution = 0;

        joel4 = new PhysicsObject(50, 70);
        joel4.Position = new Vector(0, -40);
        Add(joel4, 1);
        AxleJoint liitos = new AxleJoint(joel5, joel4, new Vector(-40, 0));
        joel5.CollisionIgnoreGroup = 1;
        joel4.CollisionIgnoreGroup = 1;

        Add(liitos);
        joel4.Image = LoadImage("joel4");
        joel4.AngularVelocity = 100;
        joel4.Mass = 0.001;
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
    void AloitaPeli()
    {

        LataaKentta1();

        
    }

    void ValitseKentta()
    {

        MultiSelectWindow kenttaValikko = new MultiSelectWindow("ValitseKentta","Metsäkenttä", "Vesikenttä", "Niittykenttä");
        //kenttaValikko.AddItemHandler(0, LataaKentta, LoadImage("metsa"));
        kenttaValikko.AddItemHandler(0, LataaKentta1);
        kenttaValikko.AddItemHandler(1, LataaKentta2);
        kenttaValikko.AddItemHandler(2, LataaKentta3);
        Add(kenttaValikko);
    }

    void LataaKentta1()
    {
        LataaHahmot();
        LataaHirvio();
        Level.Background.Image = LoadImage("kentta");
        Level.Background.FitToLevel();
    }

    void LataaKentta2()
    {
        LataaHahmot();
        LataaHirvio();
        Level.Background.Image = LoadImage("kentta2");
        Level.Background.FitToLevel();
    }

    void LataaKentta3()
    {
        LataaHahmot();
        LataaHirvio();
        Level.Background.Image = LoadImage("kentta3");
        Level.Background.FitToLevel();

    }

    void LataaHirvio()
    {
        Hirvio = new PhysicsObject(90, 180);
        Hirvio.Image = LoadImage("Hirvio");
        Hirvio.CanRotate = false;
        Hirvio.Restitution = 0;
        Hirvio.X = 500;
        Hirvio.Y = 200;
        FollowerBrain seuraajanAivot = new FollowerBrain("joel5");
        seuraajanAivot.Speed = 300;
        seuraajanAivot.DistanceFar = 600;
        seuraajanAivot.DistanceClose = 200;
        Hirvio.Brain = seuraajanAivot;
        Add(Hirvio);
        


    }


    
      
    
}
