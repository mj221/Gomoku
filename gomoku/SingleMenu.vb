Public Class SingleMenu
    Public Shared username As String        'username is displayed during the game
    Public Shared Easy As Boolean           'Easy or Hard determines which difficulty
    Public Shared Hard As Boolean

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click       'play sound effect and go back to game mode form
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        GameMode.Show()
        Me.Close()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click       'play sound effect and open forms accordingly to the clicked difficulty level
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        username = txtUsername.Text
        If Easy = True Then
            SinglePlayer.Show()
        ElseIf Hard = True Then
            HardMode.Show()
        End If
        Me.Close()
    End Sub

    Private Sub btnEasy_Click(sender As Object, e As EventArgs) Handles btnEasy.Click           'change color of clicked difficulty button (for clarification for viewers)
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        btnEasy.BackColor = Color.RosyBrown
        btnHard.BackColor = Color.Transparent
        Easy = True
        Hard = False
    End Sub

    Private Sub btnHard_Click(sender As Object, e As EventArgs) Handles btnHard.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        btnHard.BackColor = Color.RosyBrown
        btnEasy.BackColor = Color.Transparent
        Easy = False
        Hard = True
    End Sub
End Class