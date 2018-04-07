using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO
{
    public partial class Form1 : Form
    {
        private bool myMove = true;
        private int[] box;
        private int[][] moves;
        private int PLAYER_1 = -1;
        private int PLAYER_2 = -2;
        private int DIFFICULITY = 1;//1=Easy - 2=Normal - 3=Hard
        private PictureBox[] pbox;
        private bool checkWinAlgorithm(int[] indexes)
        {
            if (indexes.Length <= 2)
                return false;
            if (indexes.Contains(8))
            {
                if (indexes.Contains(4) && indexes.Contains(0))
                    return true;
                else if (indexes.Contains(7) && indexes.Contains(6))
                    return true;
                else if (indexes.Contains(5) && indexes.Contains(2))
                    return true;
            }
            if (indexes.Contains(6))
            {
                if (indexes.Contains(3) && indexes.Contains(0))
                    return true;
                else if (indexes.Contains(4) && indexes.Contains(2))
                    return true;
            }
            if (indexes.Contains(0))
            {
                if (indexes.Contains(1) && indexes.Contains(2))
                    return true;
            }
            if (indexes.Contains(4))
            {
                if (indexes.Contains(7) && indexes.Contains(1))
                    return true;
                else if (indexes.Contains(5) && indexes.Contains(3))
                    return true;
            }
            return false;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public int[] emptyBoxes()
        {
            int len = 0;
            for (int i = 0; i < 9; i++)
            {
                if (box[i] > 0)
                    len += 1;
            }
            int[] eB = new int[len];
            int index = 0;
            for(int i = 0; i < 9; i++)
            {
                if(box[i] >= 0)
                {
                    if (index >= eB.Length)
                        break;
                    eB[index] = i;
                    index += 1;
                }
            }
            return eB;
        }
        public int[] playerXboxes(int PLAYER)
        {
            int len = 0;
            foreach(int a in box)
            {
                if (a == PLAYER)
                    len += 1;
            }
            int[] new_arr = new int[len];
            int index = 0;
            for(int i = 0; i < 9; i++)
            {
                if(box[i] == PLAYER)
                {
                    new_arr[index] = i;
                    index += 1;
                }
            }
            return new_arr;
        }
        public bool hasWinner(int PLAYER)
        {
            return checkWinAlgorithm(playerXboxes(PLAYER));
        }
        
        public void pcTurn()
        {
            int box_number=0;
            table.Enabled = true;
            myMove = true;
            doDecision(ref box_number);
            box[box_number] = PLAYER_2;
            pbox[box_number].ImageLocation = "./o.png";
            if (hasWinner(PLAYER_2))
            {
                table.Enabled = false;
                DialogResult dr = MessageBox.Show("Do you want to Play again?", "You Lose", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    Application.Restart();
                else
                    Application.Exit();
            }
        }
        public void myTurn(int box_number)
        {
            if (box[box_number] >= 0)
            {
                box[box_number] = PLAYER_1;
                pbox[box_number].ImageLocation = "./x.png";
                table.Enabled = false;
                myMove = false;
                if (!hasWinner(PLAYER_1))
                    play();
                else
                {
                    table.Enabled = false;
                    DialogResult dr = MessageBox.Show("Do you want to Play again?", "You Win",MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                        Application.Restart();
                    else
                        Application.Exit();
                }
            }
            
        }
        public void doDecision(ref int box_number)
        {
            if(DIFFICULITY == 1)
            {
                int[] i = emptyBoxes();
                Random rnd = new Random();
                int random = (int)(rnd.Next(0, i.Length));
                try
                {
                    box_number = i[random];
                }
                catch
                {
                    if (i.Length == 0)
                    {
                        DialogResult dr = MessageBox.Show("Do you want to Play again?", "Equivalence !", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                            Application.Restart();
                        else
                            Application.Exit();
                    }
                }
            }else if(DIFFICULITY == 2)
            {

            }
            
            

        }
        public void play(int box_number=-1)
        {
            if (myMove)
                myTurn(box_number);
            else
                pcTurn();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            box = new int[9];
            pbox = new PictureBox[9];
            moves = new int[2][];
            for(int i = 0; i < 9; i++)
                box[i] = i;
            moves[0] = new int[5];
            moves[1] = new int[5];
            pbox[0] = tl;
            pbox[1] = tc;
            pbox[2] = tr;
            pbox[3] = cl;
            pbox[4] = cc;
            pbox[5] = cr;
            pbox[6] = bl;
            pbox[7] = bc;
            pbox[8] = br;
        }
        private void tl_Click(object sender, EventArgs e)
        {
            play(0);
        }
        private void tc_Click(object sender, EventArgs e)
        {
            play(1);
        }
        private void tr_Click(object sender, EventArgs e)
        {
            play(2);
        }
        private void cl_Click(object sender, EventArgs e)
        {
            play(3);
        }
        private void cc_Click(object sender, EventArgs e)
        {
            play(4);
        }
        private void cr_Click(object sender, EventArgs e)
        {
            play(5);
        }
        private void bl_Click(object sender, EventArgs e)
        {
            play(6);
        }
        private void bc_Click(object sender, EventArgs e)
        {
            play(7);
        }
        private void br_Click(object sender, EventArgs e)
        {
            play(8);
        }
    }
}
