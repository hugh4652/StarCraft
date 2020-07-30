using System;
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
        public new static List<List<Bitmap>> animation_sprites = new List<List<Bitmap>>();


        static Marine()
        {
            max_health = 5;
            sprite_row = 32;
            sprite_col = 14;
            sprite_width = 64;
            sprite_height = 64;

            speed = 2;

            Bitmap sprite_sheet_R = Sprite.FromFile("..\\..\\contents\\Marine\\Marine_R.gif") as Bitmap;
            Bitmap sprite_sheet_L = Sprite.FromFile("..\\..\\contents\\Marine\\Marine_L.gif") as Bitmap;

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
            form.Controls.Add(this.picture);

            p.X = x;
            p.Y = y;

            d.X = x;
            d.Y = y;

            health = max_health;
            selected = false;

            state = Condition.STATE_IDLE;
            next_state = Condition.STATE_IDLE;

            picture.Left = p.X;
            picture.Top = p.Y;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            picture.Image = (Sprite)animation_sprites[0][0];

            time_index = 0;
            rot_index = 0;
        }

        public override void Move()
        {
            if (state != Condition.STATE_MOVE)
            {
                return;
            }

            double dx = d.X - p.X;
            double dy = d.Y - p.Y;
            double theta = Math.Atan2(dy, dx);

            double move_length = speed;
            double left_length = Math.Sqrt(dx * dx + dy * dy);

            if (left_length < move_length)
            {
                time_index = 0;
                move_length = left_length;
                next_state = Condition.STATE_IDLE;
            }

            rot_index = (int)((theta + Math.PI / 2 + Math.PI / sprite_row) / (Math.PI / (sprite_row / 2)) + sprite_row);
            rot_index %= sprite_row;

            p.X += (int)(move_length * Math.Cos(theta));
            p.Y += (int)(move_length * Math.Sin(theta));
        }

    }
}
