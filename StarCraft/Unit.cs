using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarCraft
{
    class Unit: IUnit, IDisposable
    {
        // 유닛 하나당 하나의 이미지를 담을 PictureBox
        public PictureBox picture = new PictureBox();

        // 유닛 상태 관련 변수
        protected enum Condition { STATE_IDLE , STATE_ATTK, STATE_MOVE, STATE_DEAD, NULL }
        protected Condition state;
        protected Condition next_state;

        // 유닛의 위치와, 이동할 위치
        public bool selected;
        protected Point p = new Point(0, 0);
        protected Point d = new Point(0, 0);


        // 유닛의 스텟들
        protected int health;
        protected static int max_health;
        protected static int speed;


        // 이미지 관련 변수
        protected static int sprite_width;
        protected static int sprite_height;
        protected static int sprite_row;
        protected static int sprite_col;
        protected List<List<Bitmap>> animation_sprites = new List<List<Bitmap>>();

        // 애니메이션 관련 변수
        protected int time_index;
        protected int rot_index;


        public void SetDestination(Point point)
        {
            Console.WriteLine($"{this}의 목표 좌표: {p}");
            next_state = Condition.STATE_MOVE;
            d.X = point.X;
            d.Y = point.Y;
        }

        public virtual void Move()
        {
            if(state != Condition.STATE_MOVE)
            {
                return;
            }

            double dx = d.X - p.X;
            double dy = d.Y - p.Y;
            double theta = Math.Atan2(dy, dx);

            double move_length = speed;
            double left_length = Math.Sqrt(dx * dx + dy * dy);

            if(left_length < move_length)
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

        public void ImageUpdate()
        {
            Debug();
            if (state == Condition.STATE_MOVE)
            {
                picture.Left = (int)p.X;
                picture.Top = (int)p.Y;
                picture.Image = (Sprite)animation_sprites[(time_index / 3) % 9 + 4][rot_index];
            }
            else if (state == Condition.STATE_ATTK)
            {
                picture.Image = (Sprite)animation_sprites[(time_index / 10) % 2 + 2][rot_index];
            }
            else if (state == Condition.STATE_IDLE)
            {
                picture.Image = (Sprite)animation_sprites[(time_index / 30) % 2][rot_index];
            }
            else if (state == Condition.STATE_DEAD)
            {
                picture.Image = (Sprite)animation_sprites[13][time_index / 5];
            }
            else
            {
                next_state = Condition.NULL;
                picture.Dispose();
                this.Dispose();
            }
        }

        public void update()
        {
            //Debug();
            if (!selected)
            {
                return;
            }
            Move();
            ImageUpdate();

            state = next_state;

            time_index += 1;
        }

        public void Debug()
        {
            Console.WriteLine($"{this}의 상태");
            Console.WriteLine($"{this} 선택 됨: {selected}");
            Console.WriteLine($"{this}의 현재 좌표: {p}");
            Console.WriteLine($"{this}의 목표 좌표: {d}");
            Console.WriteLine($"{this}의 PictureBox 좌표: {picture.Location}");
            Console.WriteLine($"{this}의 time_index 값: {time_index}");
            Console.WriteLine($"{this}의 rot_index 값: {rot_index}");
            Console.WriteLine($"{this}의 sprite_row 값: {sprite_row}");
        }

        ~Unit()
        {
            if(state == Condition.NULL)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
