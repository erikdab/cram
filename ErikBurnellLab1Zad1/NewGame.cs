using System;
using System.Windows.Forms;

namespace CRAM
{
    /// <summary>
    /// New Game Form.
    /// </summary>
    public partial class NewGame : TemplateForm
    {
        public NewGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close Form event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Start Game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            var rulerName = textBoxPlayerName.Text;
            var villageName = textBoxVillageName.Text;

            if (rulerName == "" || villageName == "")
            {
                MessageBox.Show("Village Name and Ruler Name cannot be empty!", "Empty Names");
            }
            else
            {
                // Show Scenario first
                var formScenario = new Scenario();
                Hide();
                formScenario.ShowDialog();

                // Then the game
                var formGame = new GameScreen(villageName, rulerName);
                formGame.ShowDialog();
                Close();
            }
        }
    }
}
