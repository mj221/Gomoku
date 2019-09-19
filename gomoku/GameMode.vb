Public Class GameMode


    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")        'Play sound effect 
        Dim result = MessageBox.Show(" Are you sure you want to go to the Main Menu?", "Please Confirm Before Exiting", MessageBoxButtons.YesNo)    'Confirmation message

        If result = DialogResult.Yes Then           'If Yes is clicked, the player will return to the Main Menu screen

            MainMenu.Show()
            Me.Close()                              'Current form is closed
        End If
    End Sub

    Private Sub btnSinglePlayer_Click(sender As Object, e As EventArgs) Handles btnSinglePlayer.Click
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")        'Play sound effect
        SingleMenu.Show()           'Open Single Player Menu Form
        Me.Close()                  'Close current form
    End Sub

    Private Sub btnTwoPlayer_Click(sender As Object, e As EventArgs) Handles btnTwoPlayer.Click
        My.Computer.Audio.Play("C:\Users\MJL\Desktop\GOMOKU\gomoku\gomoku\bin\Debug\Button.wav")        'Play sound effect
        TwoMenu.Show()              'Open Two Player Menu Form
        Me.Close()                  'Close current form
    End Sub
End Class