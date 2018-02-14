using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarshalClass;

namespace BattleTesterSharp
{
    public partial class Form1 : Form
    {
        private attacker attackerInfo;
        private defender defenderInfo;
        private battleResult result;
        public Form1()
        {
            InitializeComponent();
            this.textBox1.KeyPress += onTextChanged;
            this.textBox2.KeyPress += onTextChanged;
            this.textBox3.KeyPress += onTextChanged;
            this.textBox4.KeyPress += onTextChanged;
            this.textBox5.KeyPress += onTextChanged;
            this.textBox6.KeyPress += onTextChanged;
            this.textBox7.KeyPress += onTextChanged;
            this.textBox8.KeyPress += onTextChanged;
            this.textBox9.KeyPress += onTextChanged;
            this.textBox10.KeyPress += onTextChanged;
            this.textBox11.KeyPress += onTextChanged;
            this.textBox12.KeyPress += onTextChanged;
            this.textBox13.KeyPress += onTextChanged;
            this.textBox14.KeyPress += onTextChanged;
            this.textBox15.KeyPress += onTextChanged;
            this.textBox16.KeyPress += onTextChanged;
            this.textBox17.KeyPress += onTextChanged;
            this.textBox18.KeyPress += onTextChanged;
            this.textBox19.KeyPress += onTextChanged;
            this.textBox20.KeyPress += onTextChanged;
            this.textBox21.KeyPress += onTextChanged;
            this.textBox22.KeyPress += onTextChanged;
            this.textBox23.KeyPress += onTextChanged;
            this.textBox24.KeyPress += onTextChanged;
            this.textBox1.TextChanged += fieldTextChanged;
            this.textBox2.TextChanged += fieldTextChanged;
            this.textBox3.TextChanged += fieldTextChanged;
            this.textBox4.TextChanged += fieldTextChanged;
            this.textBox5.TextChanged += fieldTextChanged;
            this.textBox6.TextChanged += fieldTextChanged;
            this.textBox7.TextChanged += fieldTextChanged;
            this.textBox8.TextChanged += fieldTextChanged;
            this.textBox9.TextChanged += fieldTextChanged;
            this.textBox10.TextChanged += fieldTextChanged;
            this.textBox11.TextChanged += fieldTextChanged;
            this.textBox12.TextChanged += fieldTextChanged;
            this.textBox13.TextChanged += fieldTextChanged;
            this.textBox14.TextChanged += fieldTextChanged;
            this.textBox15.TextChanged += fieldTextChanged;
            this.textBox16.TextChanged += fieldTextChanged;
            this.textBox17.TextChanged += fieldTextChanged;
            this.textBox18.TextChanged += fieldTextChanged;
            this.textBox19.TextChanged += fieldTextChanged;
            this.textBox20.TextChanged += fieldTextChanged;
            this.textBox21.TextChanged += fieldTextChanged;
            this.textBox22.TextChanged += fieldTextChanged;
            this.textBox23.TextChanged += fieldTextChanged;
            this.textBox24.TextChanged += fieldTextChanged;
            attackerInfo = new attacker();
            attackerInfo.attack_modifier = 1;
            attackerInfo.defence_modifier = 1;
            attackerInfo.life_modifier = 1;
            attackerInfo.troops = new Int64[12];
            defenderInfo = new defender();
            defenderInfo.attack_modifier = 1;
            defenderInfo.defence_modifier = 1;
            defenderInfo.life_modifier = 1;
            defenderInfo.troops = new Int64[12];
            result = new battleResult();
            result.attackerTroops = new Int64[12];
            result.defenderTroops = new Int64[12];
            result.result = 0;
            result.fortification = new Int64[5];
        }

        private void fieldTextChanged(object sender, EventArgs e)
        {
            TableLayoutPanel x = (TableLayoutPanel)(((Control)sender).Parent);
            TableLayoutPanelCellPosition y = x.GetPositionFromControl((Control)sender);
            string z;
            if (x==this.tableLayoutPanel4)
            {
                z = ((TextBox)sender).Text.Trim();
                if (z.Length == 0) attackerInfo.troops[y.Row] = 0;
                else attackerInfo.troops[y.Row] = Convert.ToInt64(z);
            }
            else if (x==this.tableLayoutPanel5)
            {
                z = ((TextBox)sender).Text.Trim();
                if (z.Length == 0) defenderInfo.troops[y.Row] = 0;
                else defenderInfo.troops[y.Row] = Convert.ToInt64(z);
            }
            MarshalClass.Wrapper.simulateBattle(attackerInfo, defenderInfo, out result);
            for (int i=0;i<12;i++)
            {
                ((Label)this.tableLayoutPanel4.GetControlFromPosition(2, i)).Text = (attackerInfo.troops[i]-result.attackerTroops[i]).ToString();
                ((Label)this.tableLayoutPanel5.GetControlFromPosition(2, i)).Text = (defenderInfo.troops[i]-result.defenderTroops[i]).ToString();
            }
            if (result.result == 1) this.label57.Text = "Attackers won, fight lasted " + result.totalRounds + " rounds";
            else if (result.result == 2) this.label57.Text = "Defenders won, fight lasted " + result.totalRounds + " rounds";
            else this.label57.Text = "Ended in a draw, fight lasted " + result.totalRounds + " rounds";
        }

        private void onTextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
