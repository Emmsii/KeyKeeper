using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace KeyKeeper.Graphics
{
    public class Font : Spritesheet
    {

        private readonly string _charset;
        private readonly Sprite[] _sprites;
        private readonly int _charactersWide;
        private readonly int _charactersHigh;

        public Font(string charset, int characterWidth, int characterHeight, Texture2D texture) : base(characterWidth, characterHeight, texture)
        {
            _charset = charset;
            _charactersWide = texture.Width / characterWidth;
            _charactersHigh = texture.Height / characterHeight;
            _sprites = new Sprite[_charactersWide * _charactersHigh];

            LoadCharacterSprites(_charset);
        }

        private void LoadCharacterSprites(string charset)
        {
            for(int y = 0; y < _charactersHigh; y++)
            {
                for(int x = 0; x < _charactersWide; x++)
                {
                    _sprites[x + y * _charactersWide] = CutSprite(x, y, 1, $"char_{charset[x + y * _charactersWide]}_{x}_{y}");
                }
            }
        }

        public Sprite GetCharacterSprite(char character)
        {
            int index = _charset.IndexOf(character);
            if(index < 0 || index >= _sprites.Length)
            {
                throw new IndexOutOfRangeException($"Invalid character: {character}.");
            }

            return _sprites[index];
        }
    }
}
