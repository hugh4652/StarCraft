using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarCraft
{
    class Unit: IUnit
    {
        // 유닛 하나당 하나의 이미지를 담을 PictureBox
        public PictureBox picture = new PictureBox();

        // 유닛 상태 관련 변수
        protected enum Condition { STATE_IDLE , STATE_ATTK, STATE_MOVE, STATE_DEAD, NULL }
        protected Condition state;
        protected Condition next_state;

        // 유닛의 위치와, 이동할 위치
        protected Point p;
        protected Point des;


        // 유닛의 스텟들
        protected int health;
        protected static int max_health;
        protected static int speed;


        // 이미지 관련 변수
        protected static int sprite_width;
        protected static int sprite_height;
        protected static List<List<Bitmap>> animation_sprites = new List<List<Bitmap>>();

        // 애니메이션 관련 변수
        protected int time_index;
        protected int rot_index;


    }
}
