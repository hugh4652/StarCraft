using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarCraft
{
    class Sprite
    {
        public static List<List<Bitmap>> LoadUnitSprite(string name, int row, int col)
        {
            List<List<Bitmap>> animation_sprites = new List<List<Bitmap>>();
            Bitmap image;

            for (int j = 0; j < col; j++)
            {
                List<Bitmap> temp_list = new List<Bitmap>();

                for (int i = 0; i < row; i++)
                {
                    image = Image.FromFile($"..\\..\\contents\\{name}\\{name}_{j}_{i}.gif") as Bitmap;
                    temp_list.Add(image);
                }

                animation_sprites.Add(temp_list);
            }

            return animation_sprites;
        }
    }
}
