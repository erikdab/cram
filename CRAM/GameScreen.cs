using System;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace CRAM
{
    /// <summary>
    /// Game Speed Enum.
    /// </summary>
    internal enum GameSpeed
    {
        Slower,
        Normal,
        Faster
    }

    /// <summary>
    /// Game Play Form.
    /// </summary>
    public partial class GameScreen : TemplateForm
    {
        /// <summary>
        /// The player's kingdom.
        /// </summary>
        Kingdom _kingdom = new Kingdom();

        /// <summary>
        /// Current Game Speed
        /// </summary>
        GameSpeed _gameSpeed = GameSpeed.Normal;

        /// <summary>
        /// Currently selected district.
        /// </summary>
        private District _selectedDistrict;

        /// <summary>
        /// Game Time Second
        /// </summary>
        private int _gameSecond = 0;

        /// <summary>
        /// Game Time Minute
        /// </summary>
        private int _gameMinute = 0;

        /// <summary>
        /// Game Time Hour
        /// </summary>
        private int _gameHour = 0;

        /// <summary>
        /// Game Constructor.
        /// </summary>
        /// <param name="villageName">Name of the Village.</param>
        /// <param name="rulerName">Name of the Village Ruler.</param>
        public GameScreen(string villageName, string rulerName)
        {
            InitializeComponent();

            labelVillageName.Text = villageName;
            labelVillageRuler.Text = rulerName;

            _selectedDistrict = _kingdom.FarmDistrict;
            UpdateCurrentDistrictInfo();
            SetGameSpeed(_gameSpeed);
            UpdateControls();

            GoFullscreen(true);
        }

        /// <summary>
        /// Upgrade Level in roman literals. (for display only)
        /// </summary>
        /// <returns></returns>
        private static string IntegerInRomanLiterals(int number)
        {
            switch (number)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Timer for the Game. Actions are performed during every tick, like
        /// resource production and upgrading buildings.
        /// Also, checks game victory condition.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGame_Tick(object sender, EventArgs e)
        {
            if (_kingdom.CastleDistrict.UpgradeLevel == _kingdom.CastleDistrict.MaxUpgradeLevel)
            {
                GameStartStop(false);
                MessageBox.Show($"Congratulations, you have completed the Castle in {labelGameTime.Text} hours! Now these lands are safe and you will be greatly rewarded!", "You won!");
                Close();
            }
            _kingdom.OnTick();
            UpdateControls();
        }

        /// <summary>
        /// Update all controls (labels, pictureboxes, etc.)
        /// </summary>
        private void UpdateControls()
        {
            UpdateResourceLabels();
            UpdateCurrentDistrictPanel();
            UpdateDistrictsMap();
            UpdateWorkersPanel();
        }

        /// <summary>
        /// Update Workers Panel.
        /// </summary>
        private void UpdateWorkersPanel()
        {
            foreach (var district in _kingdom.Districts)
            {
                if(! district.CanAssignWorkers()) continue;
                
                (Controls.Find($"button{district}WorkersUp", true)[0]).Visible = _kingdom.Resources.FreePopulation > 0;

                (Controls.Find($"button{district}WorkersDown", true)[0]).Visible = district.Workers > 0;

                ((Label)(Controls.Find($"label{district}Workers", true)[0])).Text = district.Workers.ToString();
            }
        }

        /// <summary>
        /// Update Resource Labels
        /// </summary>
        private void UpdateResourceLabels()
        {
            labelFood.Text = _kingdom.Resources.Food.ToString();
            labelWood.Text = _kingdom.Resources.Wood.ToString();
            labelStone.Text = _kingdom.Resources.Stone.ToString();
            labelGold.Text = _kingdom.Resources.Gold.ToString();
            labelPopulation.Text = _kingdom.Resources.Population + " / " + _kingdom.Resources.MaximumPopulation;
            labelFreeVillagers.Text = _kingdom.Resources.FreePopulation.ToString();
        }

        /// <summary>
        /// Update districts map.
        /// </summary>
        private void UpdateDistrictsMap()
        {
            foreach (var district in _kingdom.Districts)
            {
                var districtName = district.ToString();
                var districtUpgradeLabel = (Label)(Controls.Find($"label{districtName}Level", true)[0]);
                var districtPictureBox = (PictureBox)(Controls.Find($"pictureBox{districtName}", true)[0]);

                districtUpgradeLabel.Text = IntegerInRomanLiterals(district.UpgradeLevel);
                districtPictureBox.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject($"{districtName}{district.UpgradeLevel}");
            }
        }

        /// <summary>
        /// Update Labels for district purchases.
        /// </summary>
        /// <param name="cost">Cost of the purchase.</param>
        /// <param name="purchaseType">Type of the purchase.</param>
        /// <param name="canPurchase">Can Purchase item?</param>
        private void UpdatePurchaseCosts(Resources cost, string purchaseType, bool canPurchase)
        {
            // Can Purchase at all?
            var purchasePanel = (Panel)(Controls.Find($"panel{purchaseType}", true)[0]);
            purchasePanel.Visible = canPurchase;
            if (! canPurchase) return;

            // Have Sufficient Resources?
            var buttonPay = (Button)(Controls.Find($"buttonDistrict{purchaseType}", true)[0]);
            buttonPay.Visible = _kingdom.Resources.SufficientResources(cost);

            // Update Cost Labels, Hide unused resources and color insufficient resources red.
            string[] resourceNames = cost.ResourceNames();
            foreach (var resourceName in resourceNames)
            {
                var costValue = cost.Find(resourceName);
                var labelCost = (Label)(Controls.Find($"label{purchaseType}{resourceName}Cost", true)[0]);
                var pictureBoxCost = (PictureBox)(Controls.Find($"pictureBox{purchaseType}{resourceName}Cost", true)[0]);
                if (costValue > 0)
                {
                    labelCost.Text = costValue.ToString();
                    var resourceValue = _kingdom.Resources.Find(resourceName);
                    labelCost.ForeColor = costValue >= resourceValue ? Color.Red : SystemColors.ControlText;
                    labelCost.Visible = true;
                    pictureBoxCost.Visible = true;
                }
                else
                {
                    labelCost.Visible = false;
                    pictureBoxCost.Visible = false;
                }
            }
        }

        /// <summary>
        /// Update Current District Payment information.
        /// </summary>
        private void UpdateCurrentDistrictPayments()
        {
            var upgradeCost = _selectedDistrict.UpgradeCost();
            UpdatePurchaseCosts(upgradeCost, "Upgrade", _selectedDistrict.CanUpgrade());

            try
            {
                var purchaseCost = _selectedDistrict.PurchaseCost();
                UpdatePurchaseCosts(purchaseCost, "Purchase", _selectedDistrict.CanPurchase());
                labelPurchase.Text = _selectedDistrict.PurchaseName();
            }
            catch (NotImplementedException)
            {
                panelPurchase.Hide();
            }
        }

        /// <summary>
        /// Update Current District Information.
        /// </summary>
        private void UpdateCurrentDistrictInfo()
        {
            var districtName = _selectedDistrict.ToString();

            labelDistrictName.Text = districtName + " District";
            labelDistrictDescription.Text = _selectedDistrict.Description();
            labelDistrictLevel.Text = IntegerInRomanLiterals(_selectedDistrict.UpgradeLevel);

            pictureBoxDistrict.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject($"{districtName}Thumbnail{_selectedDistrict.UpgradeLevel}");

        }

        /// <summary>
        /// Update Current District Panel.
        /// </summary>
        private void UpdateCurrentDistrictPanel()
        {
            UpdateCurrentDistrictInfo();
            UpdateCurrentDistrictPayments();
        }

        /// <summary>
        /// Pay for District item (upgrade / purchase). If successful, update controls.
        /// </summary>
        private void DistrictPayTry(string purchaseType)
        {
            try
            {
                if (purchaseType == "Upgrade")
                {
                    _selectedDistrict.UpgradeTry();
                }
                else if(purchaseType == "Purchase")
                {
                    _selectedDistrict.PurchaseTry();
                }
                UpdateControls();
            }
            catch (PayInsufficientResourcesException ex)
            {
                MessageBox.Show($"Resources Required: {ex.CostResources}", "Insufficient Resources");
            }
            catch (DistrictAtMaxLevelException)
            {
                MessageBox.Show($"{_selectedDistrict} reached maximum upgrade level: {_selectedDistrict.MaxUpgradeLevel}", "District At Max Level");
            }
            catch (PopulationAtMaxLevelException)
            {
                MessageBox.Show($"The Village has reached maximum population: {_kingdom.Resources.MaximumPopulation}. Upgrade Housing or redistribute villagers.", "Population At Max Level");
            }
        }

        /// <summary>
        /// Upgrade District handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDistrictUpgrade_Click(object sender, EventArgs e)
        {
            DistrictPayTry("Upgrade");
        }

        private void buttonDistrictPurchase_Click(object sender, EventArgs e)
        {
            DistrictPayTry("Purchase");
        }

        /// <summary>
        /// Select Farm District.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxFarm_Click(object sender, EventArgs e)
        {
            _selectedDistrict = _kingdom.FarmDistrict;
            UpdateCurrentDistrictPanel();
        }

        /// <summary>
        /// Select Lumberjack District.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxLumberjack_Click(object sender, EventArgs e)
        {
            _selectedDistrict = _kingdom.LumberjackDistrict;
            UpdateCurrentDistrictPanel();
        }

        /// <summary>
        /// Select Castle District.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxCastle_Click(object sender, EventArgs e)
        {
            _selectedDistrict = _kingdom.CastleDistrict;
            UpdateCurrentDistrictPanel();
        }

        /// <summary>
        /// Select Housing District.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxHousing_Click(object sender, EventArgs e)
        {
            _selectedDistrict = _kingdom.HousingDistrict;
            UpdateCurrentDistrictPanel();
        }

        /// <summary>
        /// Select Mines District.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMines_Click(object sender, EventArgs e)
        {
            _selectedDistrict = _kingdom.MinesDistrict;
            UpdateCurrentDistrictPanel();
        }

        /// <summary>
        /// Play or pause game and toggle the start pause icon.
        /// </summary>
        /// <param name="start">Should be on.</param>
        private void GameStartStop(bool start)
        {
            if (start)
            {
                timerSecond.Start();
                timerGame.Start();
                buttonPlayPause.BackgroundImage = Properties.Resources.right;
            }
            else
            {
                timerSecond.Stop();
                timerGame.Stop();
                buttonPlayPause.BackgroundImage = Properties.Resources.pause;
            }
        }

        /// <summary>
        /// Close Form Button handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the game? You will have to start a new game!", "Exit?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        /// <summary>
        /// Minimize Form Button handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Toggle between Game Play/Pause.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            GameStartStop(! timerGame.Enabled);
        }

        /// <summary>
        /// Set Game Speed by changing the Game Tick Interval, updating the tooltip and image.
        /// </summary>
        /// <param name="gameSpeed"></param>
        private void SetGameSpeed(GameSpeed gameSpeed)
        {
            buttonSlower.Visible = true;
            buttonFaster.Visible = true;

            switch (gameSpeed)
            {
                case GameSpeed.Slower:
                    timerGame.Interval = 1500;
                    pictureBoxSpeed.Image = Properties.Resources.joystickLeft;
                    toolTipInfo.SetToolTip(pictureBoxSpeed, "Game Speed (Slower)");
                    buttonSlower.Visible = false;
                    break;
                case GameSpeed.Normal:
                    timerGame.Interval = 1000;
                    pictureBoxSpeed.Image = Properties.Resources.joystickUp;
                    toolTipInfo.SetToolTip(pictureBoxSpeed, "Game Speed (Normal)");
                    break;
                case GameSpeed.Faster:
                    timerGame.Interval = 500;
                    pictureBoxSpeed.Image = Properties.Resources.joystickRight;
                    toolTipInfo.SetToolTip(pictureBoxSpeed, "Game Speed (Faster)");
                    buttonFaster.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Decrease Game Speed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSlower_Click(object sender, EventArgs e)
        {
            if (_gameSpeed > GameSpeed.Slower)
            {
                _gameSpeed--;

                SetGameSpeed(_gameSpeed);
            }
        }

        /// <summary>
        /// Increase Game Speed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFaster_Click(object sender, EventArgs e)
        {
            if (_gameSpeed < GameSpeed.Faster)
            {
                _gameSpeed++;

                SetGameSpeed(_gameSpeed);
            }
        }

        /// <summary>
        /// Increase Farm Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFarmWorkersUp_Click(object sender, EventArgs e)
        {
            _kingdom.FarmDistrict.AssignWorker();
            UpdateControls();
        }

        /// <summary>
        /// Decrease Farm Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFarmWorkersDown_Click(object sender, EventArgs e)
        {
            _kingdom.FarmDistrict.FreeWorker();
            UpdateControls();
        }

        /// <summary>
        /// Increase Lumberjack Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLumberjackWorkersUp_Click(object sender, EventArgs e)
        {
            _kingdom.LumberjackDistrict.AssignWorker();
            UpdateControls();
        }

        /// <summary>
        /// Decrease Lumberjack Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLumberjackWorkersDown_Click(object sender, EventArgs e)
        {
            _kingdom.LumberjackDistrict.FreeWorker();
            UpdateControls();
        }

        /// <summary>
        /// Increase Mines Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMinesWorkersUp_Click(object sender, EventArgs e)
        {
            _kingdom.MinesDistrict.AssignWorker();
            UpdateControls();
        }

        /// <summary>
        /// Decrease Mines Workers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMinesWorkersDown_Click(object sender, EventArgs e)
        {
            _kingdom.MinesDistrict.FreeWorker();
            UpdateControls();
        }

        /// <summary>
        /// Show the Help Screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            GameStartStop(false);
            var form = new Help();
            form.ShowDialog();
            GameStartStop(true);
        }

        /// <summary>
        /// Open Scenario Information Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScenario_Click(object sender, EventArgs e)
        {
            GameStartStop(false);
            var form = new Scenario();
            form.ShowDialog();
            GameStartStop(true);
        }

        /// <summary>
        /// Measure and display Game time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSecond_Tick(object sender, EventArgs e)
        {
            _gameSecond++;
            if (_gameSecond == 60)
            {
                _gameSecond = 0;
                _gameMinute++;
            }
            if (_gameMinute == 60)
            {
                _gameMinute = 0;
                _gameHour++;
            }
            // Oh well, if you're playing this long, restart counting.
            if (_gameHour == 24)
            {
                _gameHour = 0;
            }

            labelGameTime.Text = $"{_gameHour}:{_gameMinute:00}:{_gameSecond:00}";
        }
    }
}
