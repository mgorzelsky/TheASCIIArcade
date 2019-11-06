using System;
using System.Text;

namespace FlappyBird
{
    public class Render
    {
        public void DrawScreen()
        {
            StringBuilder screenAsString = new StringBuilder("", FlappyBirdProgram.width * FlappyBirdProgram.height);
            char currentCharacter = Convert.ToChar(32);
            for (int y = 0; y < FlappyBirdProgram.height; y++)
            {
                screenAsString.Clear();
                for (int x = 0; x < FlappyBirdProgram.width; x++)
                {
                    if (y == 0)
                        currentCharacter = '-';
                    else if (y == FlappyBirdProgram.height - 1)
                        currentCharacter = '-';
                    else if (x == 0)
                        currentCharacter = '|';
                    else if (x == FlappyBirdProgram.width - 1)
                        currentCharacter = '|';
                    else if (y > 0 && y < FlappyBirdProgram.height - 1)
                    {

                        switch (Game.state[x, y])
                        {
                            case (CellState.Empty):
                                currentCharacter = Convert.ToChar(32);
                                break;
                            case (CellState.Bird):
                                currentCharacter = '>';
                                break;
                            case (CellState.Pillar):
                                currentCharacter = '|';
                                break;
                        }
                    }
                    screenAsString.Append(new char[] { currentCharacter });

                }
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, y);
                Console.Write(screenAsString);
            }
        }
        public void DrawGenericScreen(string thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset);
            foreach (char character in thingToDraw)
            {
                if (character.Equals('\0'))
                {
                    heightOffset++;
                    Console.SetCursorPosition(widthOffset, heightOffset);
                }
                if (!character.Equals('\0'))
                    Console.Write(character);
            }
        }

        public void DrawGenericScreen(string[] thingToDraw, int widthOffset, int heightOffset)
        {
            Console.SetCursorPosition(widthOffset, heightOffset);
            foreach (string line in thingToDraw)
            {
                Console.SetCursorPosition(widthOffset, heightOffset);
                Console.Write(line);
                heightOffset++;
            }
        }
    }
}
