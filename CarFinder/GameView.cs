using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CarFinder
{
    public partial class GameView : Form, IGameView
    {
        private IGamePresenter _presenter;

        public GameView()
        {
            InitializeComponent();

            _presenter = new GamePresenter(this);
            _presenter.Setup();
        }

        public string Velocity
        {
            get
            {
                return txtVelocity.Text;
            }
            set
            {
                txtVelocity.Text = value;
            }
        }


        public string Position
        {
            get
            {
                return txtPosition.Text;
            }
            set
            {
                txtPosition.Text = value;
            }
        }

        public void ChangeToCancelMode()
        {
            btnStart.Enabled = true;
            txtPosition.ReadOnly = false;
            txtVelocity.ReadOnly = false;
            btnRandom.Enabled = true;
        }

        public void ChangeToStartMode()
        {
            btnStart.Enabled = false;
            txtPosition.ReadOnly = true;
            txtVelocity.ReadOnly = true;
            btnRandom.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _presenter.Start();
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void UpdateStatus(string status)
        {
            lblStatus.Text = status;
        }

        public string GetStatus()
        {
            return lblStatus.Text;
        }

        public void BindScores(DataTable data)
        {
            grdScores.DataSource = data;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.CancelProcess();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
          _presenter.Randomize();
        }

    }
}
