using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIC_TAC_TOE
{
    public partial class frmBoard : Form
    {
        public enum Player
        {
            X, O
        }

        Player currentPlayer; //declare current player from Player class (enum, in this case)
        List<Button> buttons; //create an array list of buttons
        Random rand = new Random(); //import the random number generator

        int playerWins = 0; //initialize player wins to 0
        int computerWins = 0; //iniialize AI wins to 0

        public frmBoard()
        {
            InitializeComponent();
            resetGame(); //reset the gameboard on load
        }

        //event-handler responsible for firing when player has clicked a button
        private void playerClick(object sender, EventArgs e)
        {
            var button = (Button)sender;                    //find which button was clicked
            currentPlayer = Player.X;                       //set the player to X
            button.Text = currentPlayer.ToString();         //set the text of the button to current player's
            button.Enabled = false;                         //disable the button once it has been clicked
            button.BackColor = System.Drawing.Color.Cyan;   //set player button color to cyan
            buttons.Remove(button);                         //remove current button from button array List (unselectable by AI)
            Check();                                        //call method checking if player won
            tmrAIMoves.Start();                                //start the AI timer
        }

        //event-handler for AI selection
        private void AIMove(object sender, EventArgs e)
        {
            if(buttons.Count > 0) {
                int index = rand.Next(buttons.Count);           //generate a random number within the count of buttons in the button list
                buttons[index].Enabled = false;                 //disable the button at the randomly generated index

                currentPlayer = Player.O;                       //set the player to O
                buttons[index].Text = "O";                      //set the text of the button to AI's
                buttons[index].BackColor = System.Drawing.Color.DarkSalmon; //AI button color to darksalmon
                buttons.RemoveAt(index);
                Check();
                tmrAIMoves.Stop();

            }
        }

        //method responsible for restarting the gameboard
        private void restartGame(object sender, EventArgs e)
        {
            resetGame(); //call the 'resetGame' method
        }

        //method responsible for adding all playable buttons to the 'buttons' array list
        private void loadButtons()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }

        //method responsible for resetting the game board
        private void resetGame()
        {
            //check each button with a tag of 'play'
            foreach(Control X in this.Controls)
            {
                if(X is Button && X.Tag == "play")
                {
                    X.Enabled = true; //enable button
                    X.Text = "?"; //change button text to "?"
                    X.BackColor = default(Color); //change button background color to default
                }
                loadButtons(); //call method recreating array list of buttons
            }
        }

        //method responsible for checking for a win or draw between player and AI
        private void Check()
        {
            //if player1 has any of the winning combinations
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
               || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
               || button7.Text == "X" && button9.Text == "X" && button8.Text == "X"
               || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
               || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
               || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
               || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
               || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                tmrAIMoves.Stop();                                  //stop the AI move timer
                MessageBox.Show("Player 1 Wins");                   //display messagebox declaring player1 as winner
                playerWins++;                                       //increment player 1 wins
                lblPlayerWins.Text = "Player Wins: " + playerWins;  //update player label to reflect score change
                resetGame();                                        //call method to reset the gameboard
            }
            //if AI has any of the winning combinations
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
            || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
            || button7.Text == "O" && button9.Text == "O" && button8.Text == "O"
            || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
            || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
            || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
            || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
            || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {   
                tmrAIMoves.Stop();                                  //stop the AI move timer
                MessageBox.Show("AI Moves");                        //display messageBox declaring AI as winner
                computerWins++;                                     //increment AI wins
                lblAIWins.Text = "AI Wins: " + computerWins;        //update computer label to reflect score change
                resetGame();                                        //run the reset the gameboard
            }
        }
    }
}
