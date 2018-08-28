namespace CheckersWindowsUI
{
    internal class WindowsUI
    {
        private readonly SettingsForm r_SettingsForm;
        private GameForm m_GameForm;

        internal WindowsUI()
        {
            r_SettingsForm = new SettingsForm();
        }

        public void StartGame()
        {
            r_SettingsForm.ShowDialog();

            if (r_SettingsForm.IsValidSettings)
            {
                m_GameForm = new GameForm(r_SettingsForm.BoardSize, r_SettingsForm.Is2PlayerMode, r_SettingsForm.Player1Name, r_SettingsForm.Player2Name);
                m_GameForm.ShowDialog();
            }
        }
    }
}
