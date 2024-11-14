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
            pontszam.Text = "Pontjaid: ";
            pontszam.Top = 30;
            pontszam.Left = 400;
            pontszam.BackColor = Color.Gray;

            
            this.Controls.Add(alma);
            alma.BackColor = Color.Red;
            alma.Top = (Lomb.Height + 5);
            alma.Left = (Lomb.Width + 5);
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
                    else if(Hand.Left == Torzs.Right)
                    {
                        UtoKez = true;
                        HandMoveTimer.Start();
                        almaFall = true; 
                    }
                }

                else if (!LeftMove)
                {
                    if(Kosar.Left != Hand.Left + 5)
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

                        else if (alma.Top == Kosar.Top)
                        {
                            LeftMove = true;
                            UtoKez = false;
                            almaFall = false;
                            num++;
                            pontszam.Text = $"Pontjaid: {num}";

                            alma.Left = (Lomb.Width + 5);
                            alma.Top = (Lomb.Height + 5);
                            Hand.Left -= 35;
                        }
                            
                    }
                }
            };
            BodyMoveTimer.Start();

            HandMoveTimer.Interval = 100;
            HandMoveTimer.Tick += (ss, ee) =>
            {
                if (Hand.Left == Torzs.Right)
                {
                    Hand.Left += 15;
                }
                else
                {
                    Hand.Left -= 15;
                }
            };


        }
        void AlmaEsikEvent()
        {
            AppleFallTimer.Interval = 10;
            
            AppleFallTimer.Tick += (s, e) =>
            {
                if (almaFall)
                {
                    alma.Top += 1;
                }
                if (alma.Bottom >= Hand.Top && LeftMove)
                {
                    HandMoveTimer.Stop();
                    Hand.Left = Body.Right - 10;
                    alma.Top = Hand.Top - 10;
                    alma.Left = Body.Right + 10;
                    almaFall = false;
                    LeftMove = false;
                }
            };
            
            AppleFallTimer.Start();
        }
    }
}
