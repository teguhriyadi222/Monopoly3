using System;

namespace monopoly
{
    public interface IPlayer
    {
        int GetID();
        string GetName();
    }

    public class Player : IPlayer
    {
        private int id;
        private string name;
        private static int nextID = 1;

        public Player(string name)
        {
            this.id = nextID++;
            this.name = name;
        }

        public int GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }
    }
}
