using System;

namespace MarblesAndMonsters.Characters
{
    public class CharacterStateEventArgs: EventArgs
    {
        public string Message { get; set; }
        CharacterStateEventArgs(string message)
        {
            Message = message;
        }
    }
}