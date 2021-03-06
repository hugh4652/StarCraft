﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarCraft
{
    public partial class MainScreen : Form
    {
        private LinkedList<Unit> unit_list = new LinkedList<Unit>();

        public MainScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Unit marine = new Marine(this, 0, 0);
            unit_list.AddLast(marine);
           
        }

        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (Unit unit in unit_list)
                {
                    unit.selected = true;
                    unit.SetDestination(e.Location);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Unit unit in unit_list)
            {
                unit.update();
            }
        }

    }
}
