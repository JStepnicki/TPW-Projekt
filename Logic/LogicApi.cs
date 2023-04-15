using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPIInstance()
        {
            return new Board(800, 600);
        }

        public abstract void CreateTasks(int quantity, int radius);

        //... i reaktywne (okresowe wysłanie położenia kul).

        public abstract void EnableMovement();
        public abstract void ClearBoard();

        public abstract List<List<int>> GetBallsPosition();










        // W INSTRUKCJI JEST COS O OKRESOWYM WYSYLANIU POLOZENIA KUL
        // nie wiem jak to interpretowac, jest opcja ze po prostu wysyla tablice z polozeniami wszystkich kul (i chyba to to)
        // moze wysyla po prostu info na temat jakiejs konkretnej kuli

        // zagwozdka jest taka, czy powinno sie wysylac obiekty kul, czy tylko x,y,radius jako zwykle floaty
        // imho to drugie



    }
}
