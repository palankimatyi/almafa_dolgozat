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
        //int utes = 0;
        //int num2 = 0;
        //int num3 = 0;

        public Form1()
        {
            InitializeComponent();
            Start();
            AlmaEsikEvent();
            BiggerBasket();
        }
        void Start()
        {
            pontszam.Text = "Gyűjtött almák száma: ";
            
            this.Controls.Add(alma);
            alma.BackColor = Color.Red;
            alma.Top = (Lomb.Height + 1);
            alma.Left = (Lomb.Width + 5);
            alma.Height = 15;
            alma.Width = 15;

            BodyMoveTimer.Interval = 1;
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
                    //else if(utes == 10)
                    //{
                       // almaFall = true; Első esésnél működött, utána nem.  
                    //}
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

                            alma.Top = (Lomb.Height + 1);
                            alma.Left = (Lomb.Width + 5);
                            Hand.Left -= 50;
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
                    //utes++;
                }
                else
                {
                    Hand.Left -= 15;
                }
            };


        }

        void BiggerBasket()
        {
            button1.Click += (s, e) =>
            {
                if (num >= 10)
                {
                    button1.Text += 2;
                };
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
