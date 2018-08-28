using System;
using System.Windows.Forms;

namespace CheckersWindowsUI
{
    public partial class SettingsForm : Form
    {
        private const string k_DefaultPlayer2Name = "Computer";
        private const int k_DefaultBoardSize = 6;
        private int m_BoardSize;
        private string m_Player1Name;
        private string m_Player2Name;
        private bool m_Is2PlayerMode;
        private bool m_IsValidSettings;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }
        }

        public bool Is2PlayerMode
        {
            get
            {
                return m_Is2PlayerMode;
            }
        }

        public bool IsValidSettings
        {
            get
            {
                return m_IsValidSettings;
            }
        }

        public SettingsForm()
        {
            m_BoardSize = k_DefaultBoardSize;
            m_Player2Name = k_DefaultPlayer2Name;

            InitializeComponent();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Equals(radioButton6X6))
            {
                m_BoardSize = 6;
            }
            else if ((sender as RadioButton).Equals(radioButton8X8))
            {
                m_BoardSize = 8;
            }
            else if ((sender as RadioButton).Equals(radioButton10X10))
            {
                m_BoardSize = 10;
            }
        }

        private void textBoxPlayer1_TextChanged(object sender, EventArgs e)
        {
            string playerName = (sender as TextBox).Text;

            if(playerName != null)
            {
                m_Player1Name = playerName;
            }
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {
            string playerName = (sender as TextBox).Text;

            if (playerName != null)
            {
                m_Player2Name = playerName;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            isValidGameSettings();

            if (!m_IsValidSettings)
            {
                MessageBox.Show("Please enter a name for all players");
            }
            else
            {
                this.Close();
            }
        }

        private void isValidGameSettings()
        {
            m_IsValidSettings = !textBoxPlayer1.Text.Equals(string.Empty);

            if (m_Is2PlayerMode)
            {
                m_IsValidSettings = m_IsValidSettings && !textBoxPlayer2.Text.Equals(string.Empty);
            }
        }

        private void checkBoxIsTwoPlayers_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = (sender as CheckBox).Checked;
            m_Is2PlayerMode = (sender as CheckBox).Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }
    }
}
