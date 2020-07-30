﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarCraft
{
    class Marine: Unit
    {
        // 유닛의 스텟들
        private new static int max_health;
        private new static int speed;


        // 이미지 관련 변수
        /*
        private static Bitmap sprite_sheet_R;
        private static Bitmap sprite_sheet_L;
        */
        private new static int sprite_width;
        private new static int sprite_height;
        private new static int sprite_row;
        private new static int sprite_col;
        private new static List<List<Bitmap>> animation_sprites = new List<List<Bitmap>>();


        static Marine()
        {
            Marine.max_health = 5;
            Marine.sprite_row = 32;
            Marine.sprite_col = 14;
            Marine.sprite_width = 64;
            Marine.sprite_height = 64;

            Marine.speed = 5;

            Bitmap sprite_sheet_R = Image.FromFile("..\\..\\contents\\Marine\\Marine_R.gif") as Bitmap;
            Bitmap sprite_sheet_L = Image.FromFile("..\\..\\contents\\Marine\\Marine_L.gif") as Bitmap;

            // animation_sprite 초기화
            for (int j = 0; j < sprite_col; j++)
            {
                List<Bitmap> temp_column = new List<Bitmap>();
                for (int i = 0; i < sprite_row; i++)
                {
                    if (i < (sprite_row / 2))
                    {
                        Bitmap croppedBitmap = new Bitmap(sprite_sheet_R);
                        croppedBitmap = croppedBitmap.Clone(new Rectangle(sprite_width * i, sprite_height * j, sprite_width, sprite_height), System.Drawing.Imaging.PixelFormat.DontCare);
                        temp_column.Add(croppedBitmap);
                    }
                    else
                    {
                        int i2 = i - (sprite_row / 2);
                        Bitmap croppedBitmap = new Bitmap(sprite_sheet_L);
                        croppedBitmap = croppedBitmap.Clone(new Rectangle(sprite_width * i2, sprite_height * j, sprite_width, sprite_height), System.Drawing.Imaging.PixelFormat.DontCare);
                        temp_column.Add(croppedBitmap);
                    }
                }
                animation_sprites.Add(temp_column);
            }
        }

        public Marine(Form form, int x = 0, int y = 0)
        {
            p.X = x;
            p.Y = y;

            d.X = x;
            d.Y = y;

            health = max_health;
            selected = false;

            state = Condition.STATE_IDLE;
            next_state = Condition.STATE_IDLE;

            form.Controls.Add(picture);

            picture.Left = p.X;
            picture.Top = p.Y;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            picture.Image = (Image)animation_sprites[0][0];

            time_index = 0;
            rot_index = 0;
        }

    }
}
