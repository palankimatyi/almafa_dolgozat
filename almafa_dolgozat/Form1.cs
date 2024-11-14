using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace almafa_dolgozat
{
    public partial class Form1 : Form
    {
        Timer BodyMoveTimer = new Timer();
        Timer HandMoveTimer = new Timer();
        Timer AppleFallTimer = new Timer();
        PictureBox alma = new PictureBox();
        bool almaFall = false;
        bool UtoKez = false;
        bool LeftMove = true;
        int num = 0;
        public Form1()
        {
            InitializeComponent();
            Start();
            AlmaEsikEvent();
        }
        void Start()
        {
            Label pontszam = new Label();
            this.Controls.Add(pontszam);
            alma.Top = (Lomb.Height + 5);
            alma.Left = (Lomb.Width + 5);
            pontszam.Top = 30;
            pontszam.Left = 400;
            pontszam.BackColor = Color.Gray;

            
            this.Controls.Add(alma);
            alma.BackColor = Color.Red;
            alma.Height = 15;
            alma.Width = 15;

            BodyMoveTimer.Interval = 15;
            BodyMoveTimer.Tick += (s, e) =>
            {
                if(LeftMove && Hand.Left >= Torzs.Right)
                {
                    if(Hand.Left > Torzs.Right && !UtoKez)
                    {
                        Hand.Left -= 1;
                        Head.Left -= 1;
                        Body.Left -= 1;
                    }
                    else if(Hand.Left == Torzs.Right && UtoKez)
                    {
                        
                        UtoKez = true;
                        HandMoveTimer.Start();
                        almaFall = true; 
                    }
                }
                else if (!LeftMove)
                {
                    if(Kosar.Left != Hand.Left - 5)
                    {
                        Hand.Left += 1;
                        Head.Left += 1;
                        Body.Left += 1;
                        alma.Left += 1;
                    }
                    else
                    {
                        if(alma.Top != Kosar.Top)
                            alma.Top += 1;

                        if (alma.Top == Kosar.Top)
                            num++;
                    }
                }
            };
            BodyMoveTimer.Start();

            HandMoveTimer.Interval = 10;
            HandMoveTimer.Tick += (ss, ee) =>
            {
                if (Hand.Left == Torzs.Right)
                {
                    Hand.Left -= 15;
                }
                else
                {
                    Hand.Left += 15;
                }
            };


        }
        void AlmaEsikEvent()
        {
            AppleFallTimer.Interval = 10;
            AppleFallTimer.Start();
            AppleFallTimer.Tick += (s, e) =>
            {
                if (Hand.Left == Torzs.Right)
                {
                    alma.Top += 1;
                }
                //else if(Hand.Top == alma.Bottom)
                //{
                    
                //}
            };
            
        }
    }
}
