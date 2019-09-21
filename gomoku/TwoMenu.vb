Public Class TwoMenu
    Public Shared p1name, p2name As String
    Dim count As Integer

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)    'play sound effect and open game mode menu
        GameMode.Show()
        Me.Close()
    End Sub

    Private Sub txtP2_TextChanged(sender As Object, e As EventArgs) Handles txtP2.TextChanged       'to avoid confusing players, I have made a text box(actually a list box) to clarify who is the black and who is the white stone user
        If txtP2.Text <> "" And txtP1.Text <> "" Then
            count = 1                                                                                   'the listbox is only visible when both players write their username
            btnNext.Enabled = True                                                                      'the players can't proceed if they don't write anything in their username textbox.
        Else
            count = 0
            btnNext.Enabled = False
            lbxTip.Visible = False
        End If
        If count = 1 Then
            lbxTip.Visible = True
        End If

    End Sub

    Private Sub txtP1_TextChanged(sender As Object, e As EventArgs) Handles txtP1.TextChanged       'similar algorithm as above 
        If txtP2.Text <> "" And txtP1.Text <> "" Then
            count = 1
            btnNext.Enabled = True
        Else
            count = 0
            btnNext.Enabled = False
            lbxTip.Visible = False
        End If
        If count = 1 Then
            lbxTip.Visible = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)           'sound effect
        p1name = txtP1.Text                                                                            'player1 and player2's usernames are inherited into the next form (the game)
        p2name = txtP2.Text
        TwoPlayer.Show()
        Me.Close()
    End Sub
End Class