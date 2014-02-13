﻿using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class Pong : PhysicsGame
{
    PhysicsObject pallo;
    Vector nopeusYlos = new Vector(0, 200);
    Vector nopeusAlas = new Vector(0, -200);
     PhysicsObject maila1;
    PhysicsObject maila2;

    PhysicsObject vasenReuna;
    PhysicsObject oikeaReuna;

    IntMeter pelaajan1Pisteet;
    IntMeter pelaajan2Pisteet;

    
    public override void Begin()
    {
        LuoKentta();
        AloitaPeli();
        LisaaLaskurit();
        Vector impulssi = new Vector(150.0, 0.0);
        pallo.Hit(impulssi);
        AsetaOhjaimet();


    }

    void LuoKentta()
    {
        pallo = new PhysicsObject(40.0, 40.0);
        pallo.Shape = Shape.Circle;
        Add(pallo);
        
        maila1 = LuoMaila(Level.Left + 20.0, 0.0);
        maila2 = LuoMaila(Level.Right - 20.0, 0.0);

        pallo.X = -200.0;
        pallo.Y = 0.0;

        Vector impulssi = new Vector(150.0, 0.0);
        pallo.Hit(impulssi);
        pallo.Restitution = 2.0;

         vasenReuna = Level.CreateLeftBorder();
        vasenReuna.Restitution = 1.0;
        vasenReuna.IsVisible = false;
         oikeaReuna = Level.CreateRightBorder();
        oikeaReuna.Restitution = 1.0;
        oikeaReuna.IsVisible = false;
        PhysicsObject ylaReuna = Level.CreateTopBorder();
        ylaReuna.Restitution = 1.0;
        ylaReuna.KineticFriction = 0.0;
        ylaReuna.IsVisible = false;
        PhysicsObject alaReuna = Level.CreateBottomBorder();
        alaReuna.Restitution = 1.0;
        alaReuna.IsVisible = false;
        alaReuna.KineticFriction = 0.0;
        
        
        Level.Background.Color = Color.Black;

        Camera.ZoomToLevel();

        AddCollisionHandler(pallo, KasittelePallonTormays);
    }

    void AloitaPeli()
    {
        Vector impulssi = new Vector(150.0, 0.0);
        pallo.Hit(impulssi);

    }
    PhysicsObject LuoMaila(double x, double y)
    {
        PhysicsObject uusimaila = PhysicsObject.CreateStaticObject(20.0, 100.0);
        uusimaila.Shape = Shape.Rectangle;
        uusimaila.X = x;
        uusimaila.Y = y;
        uusimaila.Restitution = 1.0;
        Add(uusimaila);
        return uusimaila;
    }
    void AsetaOhjaimet()
    {
        Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", maila1, nopeusYlos);
        Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero) ;
        Keyboard.Listen(Key.Z, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", maila1, nopeusAlas);
        Keyboard.Listen(Key.Z, ButtonState.Released, AsetaNopeus, null, maila1, Vector.Zero);

        Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", maila2, nopeusYlos);
        Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero);
        Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", maila2, nopeusAlas);
        Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, maila2, Vector.Zero);

        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

}
    void AsetaNopeus(PhysicsObject maila, Vector nopeus)
    {
        if  ((nopeus.Y < 0)  &&  (maila.Bottom < Level.Bottom))
        {
            maila.Velocity = Vector.Zero;
            return;
        
        }
        if ((nopeus.Y > 0) && (maila.Top > Level.Top))
        {
            maila.Velocity = Vector.Zero;
            return;
        }
        maila.Velocity = nopeus;
    }


    void LisaaLaskurit()
    {
        pelaajan1Pisteet = LuoPisteLaskuri(Screen.Left + 100.0, Screen.Top - 100.0);
        pelaajan2Pisteet = LuoPisteLaskuri(Screen.Right - 100.0, Screen.Top - 100.0);
    }

    IntMeter LuoPisteLaskuri(double x, double y)
    {
        IntMeter laskuri = new IntMeter(0);
        laskuri.MaxValue = 10;

        Label naytto = new Label();
        naytto.BindTo(laskuri);
        naytto.X = x;
        naytto.Y = y;
        naytto.TextColor = Color.White;
        naytto.BorderColor = Level.Background.Color;
        naytto.Color = Level.Background.Color;
        Add(naytto);



        return laskuri;
    }
    void KasittelePallonTormays(PhysicsObject pallo, PhysicsObject kohde)
    {
        if (kohde == oikeaReuna)
        {
            pelaajan1Pisteet.Value += 1;
        }
        else if (kohde == vasenReuna)
        {
            pelaajan2Pisteet.Value += 1;
        }
    
    }
}


