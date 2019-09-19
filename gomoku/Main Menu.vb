Public Class MainMenu
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click   'Exit Application when Exit button is clicked. Also resume background music
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        Application.Exit()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click   'Currently the game cannot load a save file however the UI was implemented. This algorithm was going to allow the player to load a saved state 
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")

        MessageBox.Show("This function has not been properly implemented in the current version of GOMOKU v.1", "Notice")
        Dim OpenFileDialog1 As New OpenFileDialog()
        OpenFileDialog1.Filter = "XML File|*.xml|All Files|*."
        OpenFileDialog1.Title = "Load Game State"
        OpenFileDialog1.ShowDialog()
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click     'Opens up GameMode Form
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        GameMode.Show()

    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click     'Opens About Form 
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")

        About.Show()
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click       'Opens Help Form
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        Help.Show()
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub

    Private Sub btnMatch_Click(sender As Object, e As EventArgs) Handles btnMatch.Click     'Opens Match History Form
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")
        MatchHistory.Show()
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub

    Private Sub MainMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing    'Asks for confirmation when the user tries to leave
        Dim Result As DialogResult
        Result = MessageBox.Show("Do you wish to close this application?", "Close Application?", MessageBoxButtons.YesNo)
        If Result = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
        End If

    End Sub

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load         'Start playing background music when form is loaded
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click       'Allow the user to click to play the background music in case the music does not play
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Background.wav")
    End Sub
End Class