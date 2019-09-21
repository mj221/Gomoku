Public Class TwoPlayer
    Public Shared Data(50) As String
    Public Shared DataNum As Integer = 0
    Dim Board(19, 19) As Button
    Dim turn As Boolean = True
    Dim timer As Integer = 0
    Dim count As Integer = 0
    Dim placementlimit = 0
    Dim CheckResult As Boolean = False
    Dim Players As Boolean

    Private Sub TwoPlayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        creategame()                                                                            'creating game
        lblUsers.Text = TwoMenu.p1name & " (player1) vs. " & TwoMenu.p2name & " (player2)"      'Displaying P1 and P2 Username

    End Sub
    Public Sub creategame()         'creating a 19x19 board
        For i = 1 To 19
            For j = 1 To 19
                Board(i, j) = New Button

                Board(i, j).Text = ""
                Board(i, j).BackColor = Color.RosyBrown
                Board(i, j).FlatStyle = FlatStyle.Flat
                Board(i, j).FlatAppearance.BorderColor = Color.Black
                Board(i, j).Top = 27 + (20 * i)
                Board(i, j).Left = 50 + (20 * j)
                Board(i, j).Width = 20
                Board(i, j).Height = 20
                Controls.Add(Board(i, j))

                AddHandler Board(i, j).Click, AddressOf board_click
            Next
        Next

    End Sub
    Private Sub board_click(sender As Object, e As EventArgs)
        If turn = True Then
            'when turn is true, it is player1's turn
            sender.Text = "X"
            My.Computer.Audio.Play(My.Resources.Stone, AudioPlayMode.Background)
            sender.enabled = False
            sender.BackColor = Color.Black
            turn = False
            txtTurn.Text = "Player 2 Turn"
            checkwin()

        ElseIf turn = False Then
            sender.Text = " "               'when turn is false, it is player2's turn
            My.Computer.Audio.Play(My.Resources.Stone, AudioPlayMode.Background)
            sender.enabled = False
            sender.Backcolor = Color.White
            sender.ForeColor = Color.White
            turn = True
            txtTurn.Text = "Player 1 Turn"
            checkwin()
        End If

        If CheckResult = True Then              'This algorithm is explained in the HardMode Form. The ModeNum is a different value however. 
            tmrGame.Enabled = False
            Dim result = MessageBox.Show("Click 'Yes' to go to the Main Menu or 'No' to Cancel (However you will have to reset the board to continue playing.)", "Please Confirm", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                tmrGame.Enabled = False
                If Players = True Then
                    Data(DataNum) = TwoMenu.p1name & "(P1) vs. " & TwoMenu.p2name & "(P2)  |  " & "P1 Win " & "  |  " & "Timer: " & timer & " seconds"

                    MatchHistory.MatchAvail = True
                    MatchHistory.ModeNum = 1
                ElseIf Players = False Then
                    Data(DataNum) = TwoMenu.p1name & "(P1) vs. " & TwoMenu.p2name & "(P2)  |  " & "P2 Win " & "  |  " & "Timer: " & timer & " seconds"

                    MatchHistory.MatchAvail = True
                    MatchHistory.ModeNum = 1
                End If
                MainMenu.Show()
                Me.Close()
            ElseIf result = DialogResult.No Then
                For i = 1 To 19
                    For j = 1 To 19
                        Board(i, j).Enabled = False
                    Next
                Next
            End If
        End If




    End Sub



    Public Sub checkwin()           'algorithm explained previously in HardMode Form
        placementlimit += 1
        'for p1
        For i = 1 To 15
            For j = 1 To 15
                If Board(i, j).Text = "X" And Board(i + 1, j + 1).Text = "X" And Board(i + 2, j + 2).Text = "X" And Board(i + 3, j + 3).Text = "X" And Board(i + 4, j + 4).Text = "X" Then
                    MessageBox.Show("P1 " & TwoMenu.p1name & " Win")
                    Players = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 1 To 19
                If Board(i, j).Text = "X" And Board(i + 1, j).Text = "X" And Board(i + 2, j).Text = "X" And Board(i + 3, j).Text = "X" And Board(i + 4, j).Text = "X" Then
                    MessageBox.Show("P1 " & TwoMenu.p1name & " Win")
                    Players = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = "X" And Board(i, j + 1).Text = "X" And Board(i, j + 2).Text = "X" And Board(i, j + 3).Text = "X" And Board(i, j + 4).Text = "X" Then
                    MessageBox.Show("P1 " & TwoMenu.p1name & " Win")
                    Players = True
                    CheckResult = True
                End If
            Next
        Next
        For i = 5 To 15
            For j = 5 To 19
                If Board(i, j).Text = "X" And Board(i + 1, j - 1).Text = "X" And Board(i + 2, j - 2).Text = "X" And Board(i + 3, j - 3).Text = "X" And Board(i + 4, j - 4).Text = "X" Then
                    MessageBox.Show("P1 " & TwoMenu.p1name & " Win")
                    Players = True
                    CheckResult = True
                End If
            Next
        Next
        'for P2
        For i = 1 To 15
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i + 1, j + 1).Text = " " And Board(i + 2, j + 2).Text = " " And Board(i + 3, j + 3).Text = " " And Board(i + 4, j + 4).Text = " " Then
                    MessageBox.Show("P2 " & TwoMenu.p2name & " Win")
                    Players = False
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 15
            For j = 1 To 19
                If Board(i, j).Text = " " And Board(i + 1, j).Text = " " And Board(i + 2, j).Text = " " And Board(i + 3, j).Text = " " And Board(i + 4, j).Text = " " Then
                    MessageBox.Show("P2 " & TwoMenu.p2name & " Win")
                    Players = False
                    CheckResult = True
                End If
            Next
        Next
        For i = 1 To 19
            For j = 1 To 15
                If Board(i, j).Text = " " And Board(i, j + 1).Text = " " And Board(i, j + 2).Text = " " And Board(i, j + 3).Text = " " And Board(i, j + 4).Text = " " Then
                    MessageBox.Show("P2 " & TwoMenu.p2name & " Win")
                    Players = False
                    CheckResult = True
                End If
            Next
        Next
        For i = 5 To 15
            For j = 5 To 19
                If Board(i, j).Text = " " And Board(i + 1, j - 1).Text = " " And Board(i + 2, j - 2).Text = " " And Board(i + 3, j - 3).Text = " " And Board(i + 4, j - 4).Text = " " Then
                    MessageBox.Show("P2 " & TwoMenu.p2name & " Win")
                    Players = False
                    CheckResult = True
                End If
            Next
        Next
        If placementlimit = 180 Then
            MessageBox.Show("You have reached the maximum number of placement available (In this situation, this match won't be listed in 'Match History'. You can restart the game after clicking OK to Confirm")
            CheckResult = False
            For i = 1 To 19
                For j = 1 To 19
                    Board(i, j).Text = ""
                    Board(i, j).Enabled = True
                    Board(i, j).BackColor = Color.RosyBrown
                Next
            Next
            tmrGame.Enabled = True
            timer = 0
            turn = True
            txtTurn.Text = "Player 1 Turn"
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        MessageBox.Show("This function has not been properly implemented in the current version of GOMOKU v.1", "Notice")
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "XML File|*.xml|All Files|*."
        saveFileDialog1.Title = "Save Game State"
        saveFileDialog1.ShowDialog()
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        MessageBox.Show("This function has not been properly implemented in the current version of GOMOKU v.1", "Notice")
        Dim OpenFileDialog1 As New OpenFileDialog()
        OpenFileDialog1.Filter = "XML File|*.xml|All Files|*."
        OpenFileDialog1.Title = "Load Game State"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub tmrGame_Tick(sender As Object, e As EventArgs) Handles tmrGame.Tick
        timer += 1
        lblTimer.Text = "Time Elapsed: " & timer & " seconds"
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click
        Help.Show()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            GameMode.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub btnMainMenu_Click(sender As Object, e As EventArgs) Handles btnMainMenu.Click
        My.Computer.Audio.Play(My.Resources.Button, AudioPlayMode.Background)
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub MainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MainToolStripMenuItem.Click
        Dim result = MessageBox.Show(" Do you wish to go to the Main Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub GameModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GameModeToolStripMenuItem.Click
        Dim result = MessageBox.Show(" Do you wish to go to the Game Menu? All progress will be lost.", "Confirm", MessageBoxButtons.YesNo)
        tmrGame.Enabled = False
        If result = DialogResult.Yes Then

            MainMenu.Show()
            Me.Close()
        Else
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        CheckResult = False
        For i = 1 To 19
            For j = 1 To 19
                Board(i, j).Text = ""
                Board(i, j).Enabled = True
                Board(i, j).BackColor = Color.RosyBrown
            Next
        Next
        tmrGame.Enabled = True
        timer = 0
        turn = True
        txtTurn.Text = "Player 1 Turn"

    End Sub

    Private Sub TwoPlayer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim Result As DialogResult
        tmrGame.Enabled = False
        Result = MessageBox.Show("All progress will be lost if not saved", "Please Confirm Again", MessageBoxButtons.YesNo)

        If Result = DialogResult.No Then
            e.Cancel = True
            tmrGame.Enabled = True
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        My.Computer.Audio.Play(My.Resources.Background, AudioPlayMode.BackgroundLoop)
    End Sub
End Class


